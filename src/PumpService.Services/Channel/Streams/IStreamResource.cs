namespace PumpService.Services.Channel.Streams
{
    public interface IStreamResource : IDisposable
    {
        int InfiniteTimeout { get; }

        int ReadTimeout { get; set; }

        int WriteTimeout { get; set; }

        void DiscardInBuffer();

        void DiscardOutBuffer();

        int Read(byte[] buffer, int offset, int count);

        void Write(byte[] buffer, int offset, int count);
    }
}