namespace SharpEval.Core.IO.Streams;
internal sealed class RandomStream : Stream
{
    private long _position;

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => long.MaxValue;

    public override long Position 
    {
        get => _position;
        set => throw new NotSupportedException();
    }

    public override void Flush()
    {
        throw new NotSupportedException();
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        byte[] generated = new byte[count - offset];
        Random.Shared.NextBytes(generated);
        Array.Copy(generated, 0, buffer, 0, generated.Length);
        _position += generated.Length;
        return generated.Length;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new NotSupportedException();
    }

    public override void SetLength(long value)
    {
        throw new NotSupportedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException();
    }
}
