using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Results;
using PumpService.Services.Channel.Devices;
using PumpService.Services.Channel.Pumps.Messages.DCR;
using PumpService.Services.Channel.Pumps.Transport;
using PumpService.Services.Channel.Streams;
using PumpService.Services.Channel.Utility;
using Serilog;

namespace PumpService.Services.Channel.Pumps
{
    public class PumpSerialDevice : SerialDevice
    {
        #region Fields

        private SerialPortAdapter _serialPortAdapter;
        private Core.Domain.Devices.Device _fillingPoint;
        private byte? _abuAddress, _cpuId, _nozzleId;
        private Dictionary<byte, decimal> _nozzleIdUnitPrices;

        //private IMemoryCache _memoryCache = (IMemoryCache)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IMemoryCache));
        //private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.NeverRemove);
        //private IDistributedCache _distributedCache = (IDistributedCache)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IDistributedCache));
        private ChannelData _channelData = (ChannelData)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ChannelData));

        #endregion Fields

        #region Constructor

        public PumpSerialDevice(SerialTransport transport) : base(transport)
        {
        }

        public PumpSerialDevice(IStreamResource streamResource) : this(new DCRTransport(streamResource))
        {
            DCRTransport transport = (DCRTransport)Transport;
            transport.setDevice(this);
        }

        public PumpSerialDevice(SerialPortAdapter serialPortAdapter, Core.Domain.Devices.Device fillingPoint) : this(serialPortAdapter)
        {
            _serialPortAdapter = serialPortAdapter;
            _fillingPoint = fillingPoint;
            _nozzleId = 1;
        }

        #endregion Constructor

        #region Methods

        public void ProcessPump()
        {
            if (_abuAddress != null && _cpuId != null && _nozzleId != null)
            {
                var pumpStatusResponseMessage = RequestPumpStatus((byte)_abuAddress, (byte)_cpuId, (byte)_nozzleId);
                var result = ProcessPumpStatusResponseMessage(pumpStatusResponseMessage);
                SetPumpStatus(result);

                var channelKey = string.Join(ChannelKeys.KeySeperator, _abuAddress.ToString(), _cpuId.ToString());

                if (_channelData.PumpStatuses != null && _channelData.PumpStatuses[channelKey] != null && _channelData.PumpStatuses.ContainsKey(channelKey))
                {
                    #region Idle State

                    if (EnumClasses.DcrStates.IdleState.Equals(_channelData.PumpStatuses[channelKey].Status))
                    {
                        //todo açık satış varsa ProcessFillingInformation((byte)_abuAddress, (byte)_cpuId, (byte)_nozzleId, true);

                        _channelData.IsUpdateUnitPrice[channelKey] = true;
                    }

                    #endregion Idle State

                    #region Call State

                    else if (EnumClasses.DcrStates.CallState.Equals(_channelData.PumpStatuses[channelKey].Status))
                    {
                        //todo açık satış varsa ProcessFillingInformation((byte)_abuAddress, (byte)_cpuId, (byte)_nozzleId, true);

                        if (_channelData.IsUpdateUnitPrice[channelKey] && _nozzleIdUnitPrices != null)
                        {
                            if (UpdateUnitPrice((byte)_abuAddress, (byte)_cpuId, _nozzleIdUnitPrices))
                            {
                                _channelData.IsUpdateUnitPrice[channelKey] = false;
                            }
                        }

                        if (!_channelData.IsUpdateUnitPrice[channelKey])
                        {
                            if (_channelData.IsAuthorize != null && _channelData.IsAuthorize.ContainsKey(channelKey) && _channelData.IsAuthorize[channelKey] == true)
                            {
                                if (Authorize((byte)_abuAddress, (byte)_cpuId, (byte)_nozzleId))
                                {
                                    _channelData.IsAuthorize[channelKey] = false;
                                }
                            }
                        }
                    }

                    #endregion Call State

                    #region Busy State

                    else if (EnumClasses.DcrStates.BusyState.Equals(_channelData.PumpStatuses[channelKey].Status))
                    {
                        ProcessFillingInformation((byte)_abuAddress, (byte)_cpuId, (byte)_nozzleId, false, channelKey);
                    }

                    #endregion Busy State

                    #region Unpaid State

                    else if (EnumClasses.DcrStates.UnpaidState.Equals(_channelData.PumpStatuses[channelKey].Status))
                    {
                        ProcessFillingInformation((byte)_abuAddress, (byte)_cpuId, (byte)_nozzleId, true, channelKey);
                    }

                    #endregion Unpaid State

                    #region Paid State

                    else if (EnumClasses.DcrStates.PaidState.Equals(_channelData.PumpStatuses[channelKey].Status))
                    {
                        _channelData.IsUpdateUnitPrice[channelKey] = true;
                    }

                    #endregion Paid State
                }
            }
            else
            {
                Log.Logger.ForContext("LogKey", LogKeys.AbuAddressOrCpuIdOrNozzleNull).Warning("Message=" + LogKeys.AbuAddressOrCpuIdOrNozzleNull);
            }
        }

        #region PumpStatus

        private DCRResponseMessage? RequestPumpStatus(byte abuAddress, byte cpuId, byte nozzleId)
        {
            //Log.Logger.ForContext("LogKey", LogKeys.RequestPumpStatus).Information("RequestPumpStatus for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId);

            int timeoutCounter = 0;

            //if (EnumClasses.FillingPointStatus.FPConnectionClosed.Equals(DolumNoktasi.State) ||
            //    connectionTimeoutCounter % 3 == 0)
            //    timeoutCounter = 8;

            while (true)
            {
                var requestPumpStatusMessage = new PumpStatusMessage(abuAddress, cpuId, nozzleId);

                try
                {
                    var response = Transport.UnicastMessage<DCRResponseMessage>(requestPumpStatusMessage);

                    if (response != null && response.IsAcknowledgeMessage())
                    {
                        int expectedMessageFlag = 0;

                        while (expectedMessageFlag++ < 10)
                        {
                            var pollMessage = new PollMessage(abuAddress);
                            response = Transport.UnicastMessage<DCRResponseMessage>(pollMessage);

                            if (response != null && response.Transactions != null)
                            {
                                foreach (TransactionData trx in response.Transactions)
                                {
                                    switch (trx.TransactionId)
                                    {
                                        case 0x80:
                                            DCRResponseMessage responseMessage = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress)); // Send an ACK that the message has been received;
                                            return response;
                                    }
                                }
                            }

                            Thread.Sleep(50);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.RequestPumpStatusException).Error("Got DCR Exception during RequestPumpStatus for abuAddress=" + abuAddress + " cpuId=" + cpuId + " Message=" + e.Message + " StackTrace=" + e.StackTrace);
                }

                Thread.Sleep(50);

                if (timeoutCounter++ > 10)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.RequestPumpStatusTimeout).Error("RequestPumpStatus was tried 10 times for abuAddress=" + abuAddress + " cpuId=" + cpuId);

                    return null;
                }
            }
        }

        private Tuple<EnumClasses.DcrStates?, byte, byte>? ProcessPumpStatusResponseMessage(DCRResponseMessage? message)
        {
            if (message != null && message.Transactions != null)
            {
                foreach (TransactionData trx in message.Transactions)
                {
                    if (trx.TransactionId == 0x80)
                    {
                        var cpuIdNozzleId = DcrUtilities.GetCpuIdAndNozzleId(trx.Data[0]);
                        var pumpStatus = trx.Data[1];

                        if (_cpuId == cpuIdNozzleId.Item1)
                        {
                            return new Tuple<EnumClasses.DcrStates?, byte, byte>((EnumClasses.DcrStates)(EnumClasses.DcrStates?)pumpStatus, cpuIdNozzleId.Item1, cpuIdNozzleId.Item2);
                        }
                    }
                }
            }

            return null;
        }

        private void SetPumpStatus(Tuple<EnumClasses.DcrStates?, byte, byte>? result)
        {
            if (result != null)
            {
                _nozzleId = result.Item3;
                _channelData.PumpStatuses[string.Join(ChannelKeys.KeySeperator, _abuAddress.ToString(), _cpuId.ToString())] = new PumpStatusResult() { Status = result.Item1, AbuAddress = (byte)_abuAddress, CpuId = (byte)_cpuId, NozzleId = (byte)_nozzleId };
            }
            else
            {
                _channelData.PumpStatuses[string.Join(ChannelKeys.KeySeperator, _abuAddress.ToString(), _cpuId.ToString())] = null;
            }
        }

        #endregion PumpStatus

        #region Authorize

        private bool Authorize(byte abuAddress, byte cpuId, byte nozzleId)
        {
            Log.Logger.ForContext("LogKey", LogKeys.Authorize).Information("Authorize for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId);

            var authorizeMessage = CreateAuthorizeMessage(abuAddress, cpuId, nozzleId);

            int timeoutCounter = 0;

            while (true)
            {
                try
                {
                    Thread.Sleep(50);
                    DCRResponseMessage response = Transport.UnicastMessage<DCRResponseMessage>(authorizeMessage);
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.AuthorizeException).Error("Got DCR Exception During Authorize for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId + " Counter=" + timeoutCounter + "Message=" + ex.Message + "StackTrace=" + ex.StackTrace);
                }

                if (timeoutCounter++ > 10)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.AuthorizeTimeout).Error("Authorize was tried 10 times for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId);

                    return false;
                }
            }
        }

        private AuthorizeMessage CreateAuthorizeMessage(byte abuAddress, byte cpuId, byte nozzleId)
        {
            AuthorizeMessage authorizeMessage = new AuthorizeMessage(abuAddress, new byte[0], new byte[0]);
            authorizeMessage.PumpAndNozzle = DcrUtilities.ConstructByteFromTwoBytes(cpuId, nozzleId);
            authorizeMessage.AuthorizationType = 0x00; //cash
            authorizeMessage.PresetType = 0x00; //unlimited

            var unlimitedBytes = System.Text.Encoding.UTF8.GetBytes(DcrUtilities.PadLeft("0", 8));
            authorizeMessage.PresetValueFirstByte = DcrUtilities.ConstructByteFromTwoBytes(unlimitedBytes[0], unlimitedBytes[1]);
            authorizeMessage.PresetValueSecondByte = DcrUtilities.ConstructByteFromTwoBytes(unlimitedBytes[2], unlimitedBytes[3]);
            authorizeMessage.PresetValueThirdByte = DcrUtilities.ConstructByteFromTwoBytes(unlimitedBytes[4], unlimitedBytes[5]);
            authorizeMessage.PresetValueFourthByte = DcrUtilities.ConstructByteFromTwoBytes(unlimitedBytes[6], unlimitedBytes[7]);

            return authorizeMessage;
        }

        #endregion Authorize

        #region RequestEndOfFilling

        private void ProcessFillingInformation(byte abuAddress, byte cpuId, byte nozzleId, bool isEndOfFilling, string channelKey)
        {
            var endOfFillingResponse = RequestEndOfFilling(abuAddress, cpuId, nozzleId);

            if (endOfFillingResponse != null && endOfFillingResponse.Transactions != null)
            {
                foreach (TransactionData fillingTransaction in endOfFillingResponse.Transactions)
                {
                    if (fillingTransaction.TransactionId == 0x83)
                    {
                        var fillingData = CrcCalc.ByteToHexStr(fillingTransaction.Data);

                        if (fillingData != null && fillingData.Length > 4)
                        {
                            var cpuIdNozzleId = DcrUtilities.GetCpuIdAndNozzleId(fillingTransaction.Data[0]);

                            if (cpuId == cpuIdNozzleId.Item1) //işlenen kayıt filling point'e aitse, bir pompada birden fazla FP varsa, response bünyesinde tüm FP'lerle ilgili bilgiler oluyor
                            {
                                double amount = -1, volume = -1, unitPrice = -1;

                                GetAmountVolumeUnitPrice(fillingData, ref amount, ref volume, ref unitPrice);

                                //eğer unitprice veya amount bilgisi okunamıyorsa işleme devam edilmez, bazen bu bilgiler yanlış geliyor zira
                                if (unitPrice == -1 || amount == -1)
                                    return;

                                //todo eski kod ileride gözden geçirilecek

                                //todo dolum sonuysa totalizer oku

                                var fillingInformationResult = new FillingInformationResult() { Amount = amount, Volume = volume, UnitPrice = unitPrice, IsEndOfFilling = isEndOfFilling };

                                if (isEndOfFilling)
                                {
                                    if (_channelData.IsConfirmPay != null && _channelData.IsConfirmPay.ContainsKey(channelKey) && _channelData.IsConfirmPay[channelKey] == true)
                                    {
                                        if (PaidConfirmed(abuAddress, cpuId, nozzleId))
                                        {
                                            _channelData.IsConfirmPay[channelKey] = false;
                                        }
                                    }
                                }

                                //Log.Logger.Information(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                                //_memoryCache.Set(string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")), fillingInformationResult);
                                _channelData.FillingInformations[string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString())] = fillingInformationResult;
                                //_distributedCache.Set(string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString(), DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")), JsonSerializer.SerializeToUtf8Bytes(fillingInformationResult));                                
                            }
                        }
                    }
                }
            }
        }

        public DCRResponseMessage? RequestEndOfFilling(byte abuAddress, byte cpuId, byte nozzleId)
        {
            Log.Logger.ForContext("LogKey", LogKeys.RequestEndOfFilling).Information("RequestFillingInfo for abuAddress=" + abuAddress + " cpuId=" + cpuId);

            DCRResponseMessage response;
            RequestEndOfFillingMessage fillingMessage;

            int timeoutCounter = 0;

            while (true)
            {
                fillingMessage = CreateRequestEndOfFillingMessage(abuAddress, cpuId, nozzleId);

                try
                {
                    response = Transport.UnicastMessage<DCRResponseMessage>(fillingMessage);

                    if (response != null && response.IsAcknowledgeMessage())
                    {
                        int expectedMessageFlag = 0;

                        while (expectedMessageFlag++ < 10) //poll 10 kez denenip sonuç alınmazsa request mesaj tekrar gönderilecek
                        {
                            var pollMessage = new PollMessage(abuAddress);
                            response = Transport.UnicastMessage<DCRResponseMessage>(pollMessage);

                            if (response != null && response.Transactions != null)
                            {
                                foreach (TransactionData trx in response.Transactions) //gelen response bir veya daha fazla transaction'dan oluşabilmektedir, her transaction ayrı işlenir#sümer#
                                {
                                    switch (trx.TransactionId) //mesaj bünyesindeki transactionId'lere göre process yapılır,teosis döküman sayfa 12'de transaction id'ler vardır#sümer#
                                    {
                                        case 0x83:
                                            DCRResponseMessage message = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress)); // Send an ACK that the message has been received;
                                            return response;
                                    }
                                }
                            }

                            Thread.Sleep(50);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.RequestFillingInfoException).Error("Got DCR Exception During RequestFillingInfo for abuAddress=" + abuAddress + " cpuId=" + cpuId + " Counter=" + timeoutCounter + "Message=" + ex.Message + "StackTrace=" + ex.StackTrace);
                }

                Thread.Sleep(50);

                if (timeoutCounter++ > 10)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.RequestFillingInfoTimeout).Error("RequestFillingInfo was tried 10 times for abuAddress=" + abuAddress + " cpuId=" + cpuId);

                    return null;
                }
            }
        }

        private RequestEndOfFillingMessage CreateRequestEndOfFillingMessage(byte abuAddress, byte cpuId, byte nozzleId)
        {
            var fillingMessage = new RequestEndOfFillingMessage(abuAddress);
            fillingMessage.PumpAndNozzle = DcrUtilities.ConstructByteFromTwoBytes(cpuId, nozzleId);

            return fillingMessage;
        }

        private void GetAmountVolumeUnitPrice(string fillingData, ref double amount, ref double volume, ref double unitPrice)
        {
            var strAmount = fillingData.Substring(4, 8);
            var strVolume = fillingData.Substring(12, 8);
            var strUnitPrice = fillingData.Substring(20, 6);

            try
            {
                if (DcrUtilities.HasDigitsOnly(strAmount))
                    amount = Convert.ToDouble(strAmount) / 100;
                else
                    amount = -1;
            }
            catch (Exception)
            {
                amount = -1;

                Log.Logger.ForContext("LogKey", LogKeys.GetAmountException).Error("Amount Error for" + strAmount);
            }

            try
            {
                if (DcrUtilities.HasDigitsOnly(strVolume))
                    volume = Convert.ToDouble(strVolume) / 100;
                else
                    volume = -1;
            }
            catch (Exception)
            {
                volume = -1;

                Log.Logger.ForContext("LogKey", LogKeys.GetVolumeException).Error("Volume Error for" + strVolume);
            }

            try
            {
                if (DcrUtilities.HasDigitsOnly(strUnitPrice))
                    unitPrice = Convert.ToDouble(strUnitPrice) / 100;//Teosis 2 hane birim fiyat 1000 yerine 100
                else
                    unitPrice = -1;
            }
            catch (Exception)
            {
                unitPrice = -1;

                Log.Logger.ForContext("LogKey", LogKeys.GetUnitPriceException).Error("UnitPrice Error for" + strUnitPrice);
            }
        }

        #endregion RequestEndOfFilling

        #region Paid Confirmed

        private bool PaidConfirmed(byte abuAddress, byte cpuId, byte nozzleId)
        {
            Log.Logger.ForContext("LogKey", LogKeys.PaidConfirmed).Information("PaidConfirmed for abuAddress=" + abuAddress + " cpuId=" + cpuId);

            PaidConfirmedMessage paidConfirmedMessage = CreatePaidConfirmedMessage(abuAddress, cpuId, nozzleId);

            int timeoutCounter = 0;

            while (true)
            {
                try
                {
                    DCRResponseMessage response = Transport.UnicastMessage<DCRResponseMessage>(paidConfirmedMessage);

                    if (response != null && response.IsAcknowledgeMessage())
                    {
                        int expectedMessageFlag = 0;

                        while (expectedMessageFlag++ < 10)
                        {
                            var pollMessage = new PollMessage(abuAddress);
                            response = Transport.UnicastMessage<DCRResponseMessage>(pollMessage);

                            if (response != null && (response.IsEOTMessage() || response.IsAcknowledgeMessage()))
                            {
                                DCRResponseMessage msg1 = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress));
                                //state = EnumClasses.DcrStates.IdleState;

                                return true;
                            }

                            if (response != null && response.Transactions != null)
                            {
                                foreach (TransactionData trx in response.Transactions)
                                {
                                    switch (trx.TransactionId)
                                    {
                                        case 0x80:
                                            DCRResponseMessage message = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress));
                                            //state = EnumClasses.DcrStates.IdleState;

                                            return true;
                                    }
                                }
                            }

                            Thread.Sleep(50);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.PaidConfirmedException).Error("Got DCR Exception During PaidConfirmed for abuAddress=" + abuAddress + " cpuId=" + cpuId + " Counter=" + timeoutCounter + "Message=" + ex.Message + "StackTrace=" + ex.StackTrace);
                }

                Thread.Sleep(50);

                if (timeoutCounter++ > 10)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.PaidConfirmedTimeout).Error("PaidConfirmed was tried 10 times for abuAddress=" + abuAddress + " cpuId=" + cpuId);

                    return false;
                }
            }
        }

        private PaidConfirmedMessage CreatePaidConfirmedMessage(byte abuAddress, byte cpuId, byte nozzleId)
        {
            var paidConfirmedMessage = new PaidConfirmedMessage(abuAddress);
            paidConfirmedMessage.PumpAndNozzle = DcrUtilities.ConstructByteFromTwoBytes(cpuId, nozzleId);

            return paidConfirmedMessage;
        }

        #endregion Paid Confirmed

        #region Update Unit Price

        public bool UpdateUnitPrice(byte abuAddress, byte cpuId, Dictionary<byte, decimal> nozzleIdPrices)
        {
            Log.Logger.ForContext("LogKey", LogKeys.UpdateUnitPrice).Information("UpdateUnitPrice for abuAddress=" + abuAddress + " cpuId=" + cpuId);

            var data = CreateUpdateUnitPriceData(cpuId, nozzleIdPrices);

            if (!data.All(singleByte => singleByte == 0))
            {
                int timeoutCounter = 0;

                while (true)
                {
                    var updateUnitPricesMessage = new UpdateUnitPriceMessage(abuAddress, data);

                    try
                    {
                        DCRResponseMessage response = Transport.UnicastMessage<DCRResponseMessage>(updateUnitPricesMessage);

                        if (response != null && response.IsAcknowledgeMessage())
                        {
                            int expectedMessageFlag = 0;

                            while (expectedMessageFlag++ < 10)
                            {
                                var pollMessage = new PollMessage(abuAddress);
                                response = Transport.UnicastMessage<DCRResponseMessage>(pollMessage);

                                if (response != null && (response.IsEOTMessage() || response.IsAcknowledgeMessage()))
                                {
                                    DCRResponseMessage msg1 = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress));
                                    return true; //success
                                }

                                if (response != null && response.Transactions != null)
                                {
                                    foreach (TransactionData trx in response.Transactions)
                                    {
                                        switch (trx.TransactionId)
                                        {
                                            case 0x80:
                                                DCRResponseMessage msg1 = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress));
                                                return true;
                                        }
                                    }
                                }

                                Thread.Sleep(50);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.UpdateUnitPriceException).Error("Got DCR Exception During UpdateUnitPrice for abuAddress=" + abuAddress + " cpuId=" + cpuId + "Counter=" + timeoutCounter + "Message=" + e.Message + "StackTrace=" + e.StackTrace);
                    }

                    Thread.Sleep(50);

                    if (timeoutCounter++ > 10)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.UpdateUnitPriceTimeout).Error("UpdateUnitPrice was tried 10 times for abuAddress=" + abuAddress + " cpuId=" + cpuId);

                        return false;
                    }
                }
            }
            else
            {
                Log.Logger.ForContext("LogKey", LogKeys.UpdateUnitPriceDataEmpty).Warning("Message=" + LogKeys.UpdateUnitPriceDataEmpty);
            }

            return false;
        }

        private byte[] CreateUpdateUnitPriceData(byte cpuId, Dictionary<byte, decimal> nozzleIdPrices)
        {
            byte[] updatePriceByteArray = Enumerable.Repeat((byte)0, 4 * 5).ToArray();

            if (nozzleIdPrices != null)
            {
                int i = 0;

                foreach (var item in nozzleIdPrices)
                {
                    updatePriceByteArray[0 + (4 * i)] = DcrUtilities.ConstructByteFromTwoBytes(cpuId, item.Key);

                    var unitPrice = (item.Value / 10).ToString();//Teosis 2 hane birim fiyat
                    unitPrice = DcrUtilities.removeVirgulAndPad(unitPrice);
                    var unitPriceBytes = System.Text.Encoding.UTF8.GetBytes(unitPrice);

                    updatePriceByteArray[1 + (4 * i)] = DcrUtilities.ConstructByteFromTwoBytes(unitPriceBytes[0], unitPriceBytes[1]);
                    updatePriceByteArray[2 + (4 * i)] = DcrUtilities.ConstructByteFromTwoBytes(unitPriceBytes[2], unitPriceBytes[3]);
                    updatePriceByteArray[3 + (4 * i)] = DcrUtilities.ConstructByteFromTwoBytes(unitPriceBytes[4], unitPriceBytes[5]);

                    i++;
                }
            }

            return updatePriceByteArray;
        }
        

        #endregion Update Unit Price

        #region Nozzle Totalizer

        public decimal? GetNozzleTotalizer(byte abuAddress, byte cpuId, byte nozzleId, int? divide)
        {
            decimal? totalizer = null;

            TransactionData? nozzleTotalizer = RequestNozzleTotalizer(abuAddress, cpuId, nozzleId);

            if (nozzleTotalizer != null)
            {
                try
                {
                    var volume = CrcCalc.ByteToHexStr(nozzleTotalizer.Data).Substring(2, 8);
                    totalizer = Convert.ToDecimal(volume) / 100;
                }
                catch (Exception)
                { }
            }

            if (divide != null && totalizer != null)
            {
                totalizer = totalizer / divide;
            }

            return totalizer;
        }

        private TransactionData? RequestNozzleTotalizer(byte abuAddress, byte cpuId, byte nozzleId)
        {
            Log.Logger.ForContext("LogKey", LogKeys.NozzleTotalizer).Information("NozzleTotalizer for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId);

            byte[] data = new byte[1] { DcrUtilities.ConstructByteFromTwoBytes(cpuId, nozzleId) };

            int timeoutCounter = 0;

            while (true)
            {
                var requestNozzleTotalizer = new RequestNozzleTotalizerMessage(abuAddress, data);

                try
                {
                    var response = Transport.UnicastMessage<DCRResponseMessage>(requestNozzleTotalizer);

                    if (response != null && response.IsAcknowledgeMessage())
                    {
                        int expectedMessageFlag = 0;

                        while (expectedMessageFlag++ < 10)
                        {
                            var pollMessage = new PollMessage(abuAddress);
                            response = Transport.UnicastMessage<DCRResponseMessage>(pollMessage);

                            if (response != null && response.Transactions != null)
                            {
                                foreach (TransactionData trx in response.Transactions)
                                {
                                    switch (trx.TransactionId)
                                    {
                                        case 0x87:
                                            DCRResponseMessage msg1 = Transport.UnicastMessage<DCRResponseMessage>(new ACKMessage(abuAddress));

                                            return trx;
                                    }
                                }
                            }

                            Thread.Sleep(50);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.NozzleTotalizerException).Error("Got DCR Exception During NozzleTotalizer for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId + " Counter=" + timeoutCounter + "Message=" + e.Message + "StackTrace=" + e.StackTrace);
                }

                Thread.Sleep(50);

                if (timeoutCounter++ > 20)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.NozzleTotalizerTimeout).Error("NozzleTotalizer was tried 10 times for abuAddress=" + abuAddress + " cpuId=" + cpuId + " nozzleId=" + nozzleId);

                    return null;
                }
            }
        }

        #endregion Nozzle Totalizer

        #endregion Methods

        #region Properties

        public Core.Domain.Devices.Device GetFillingPoint()
        {
            return _fillingPoint;
        }

        public byte? AbuAddress
        {
            get { return _abuAddress; }
            set { _abuAddress = value; }
        }

        public byte? CpuId
        {
            get { return _cpuId; }
            set { _cpuId = value; }
        }

        public byte? NozzleId
        {
            get { return _nozzleId; }
            set { _nozzleId = value; }
        }

        public Dictionary<byte, decimal> NozzleIdUnitPrices
        {
            get { return _nozzleIdUnitPrices; }
            set { _nozzleIdUnitPrices = value; }
        }        

        public override void Listen()
        {
            throw new NotImplementedException();
        }

        #endregion Properties

    }
}