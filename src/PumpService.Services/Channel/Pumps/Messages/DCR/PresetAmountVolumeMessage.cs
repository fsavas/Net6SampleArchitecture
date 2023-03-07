﻿namespace PumpService.Services.Channel.Pumps.Messages.DCR
{
    internal class PresetAmountVolumeMessage : IMessage
    {
        public PresetAmountVolumeMessage(byte _address, byte[] _ProtocolDataUnitData)
        {
            SlaveAddress = _address;
            ProtocolDataUnitData = _ProtocolDataUnitData;
        }

        public byte SlaveAddress
        {
            get;
            set;
        }

        public byte[] MessageFrame
        {
            get;
            set;
        }

        public byte ControlNo
        {
            get
            {
                return 0x30;//data mesajı olduğunu gösteriyor
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte[] ProtocolDataUnitData { get; set; }

        public byte[] ProtocolDataUnit //CD127 implementation
        {
            get
            {
                Byte[] data = new Byte[ProtocolDataUnitData.Length + 2];

                data[0] = 0x7F; // TransactionId Yani Komut Kodu

                data[1] = (byte)6; // Bu Bytten sonra gelen byte sayısı

                for (int i = 0; i < ProtocolDataUnitData.Length; i++)
                {
                    data[2 + i] = ProtocolDataUnitData[i];
                }

                return data;
            }
        }

        public ushort TransactionId
        {
            get
            {
                return StateEngine.SlaveTxNo;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Initialize(byte[] frame)
        {
        }
    }
}