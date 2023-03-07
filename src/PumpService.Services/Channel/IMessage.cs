namespace PumpService.Services.Channel
{
    public interface IMessage
    {
        byte SlaveAddress { get; set; }

        /// <summary>
        /// Composition of the slave address and protocol data unit.
        /// </summary>

        byte[] MessageFrame { get; }

        byte ControlNo { get; set; }

        /// <summary>
        /// Composition of the function code and message data.

        byte[] ProtocolDataUnit { get; }

        /// <summary>
        /// A unique identifier assigned to a message when using the IP protocol.
        /// </summary>
        ushort TransactionId { get; set; }

        /// <summary>
        /// Initializes a modbus message from the specified message frame.
        /// </summary>
        /// <param name="frame">The frame.</param>
        void Initialize(byte[] frame);
    }
}