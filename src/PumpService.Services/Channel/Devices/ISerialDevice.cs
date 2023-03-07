using PumpService.Services.Channel.Streams;

namespace PumpService.Services.Channel.Devices
{
    public interface ISerialDevice
    {
        /// <summary>
        /// Transport for used by this master.
        /// </summary>
        SerialTransport Transport { get; }

        /// <summary>
        /// Serial Line only.
        /// Diagnostic function which loops back the original data.

        /// </summary>
        /// <param name="slaveAddress">Address of device to test.</param>
        /// <param name="data">Data to return.</param>
        /// <returns>Return true if slave device echoed data.</returns>
        bool ReturnQueryData(byte slaveAddress, ushort data);
    }
}