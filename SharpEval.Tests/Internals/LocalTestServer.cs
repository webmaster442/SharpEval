using System.Net;

namespace SharpEval.Tests.Internals;
internal sealed class LocalTestServer : IDisposable
{
    private readonly HttpListener _listener;
    private readonly CancellationTokenSource _tokenSource;
    private readonly Dictionary<string, (string contentType, Func<string> provider)> _handlers;

    public LocalTestServer(int port)
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://localhost:{port}/");
        _tokenSource = new CancellationTokenSource();
        _handlers = new Dictionary<string, (string contentType, Func<string> provider)>();
    }

    public void AddHandler(string file, string contentType, Func<string> contentProvider)
    {
        _handlers.Add(file, (contentType, contentProvider));
    }

    public void Dispose()
    {
        _tokenSource.Cancel();
        _listener.Stop();
        _listener.Close();
        _tokenSource.Dispose();
    }

    public void Start()
    {
        Thread t = new Thread(LitenConnections);
        _listener.Start();
        t.Start();
    }

    private void LitenConnections(object? obj)
    {
        while (true)
        {
            if (_tokenSource.IsCancellationRequested)
                break;

            try
            {
                HttpListenerContext context = _listener.GetContext();
                HttpListenerRequest req = context.Request;
                byte[] data;

                if (req.Url == null
                    || !_handlers.ContainsKey(req.Url.LocalPath))
                {

                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    context.Response.ContentType = "text/plain";
                    data = Encoding.UTF8.GetBytes("not found");
                    context.Response.ContentLength64 = data.Length;
                    context.Response.OutputStream.Write(data, 0, data.Length);
                    context.Response.Close();
                    continue;
                }
                var (contentType, provider) = _handlers[req.Url.LocalPath];

                context.Response.StatusCode = (int)HttpStatusCode.OK;
                context.Response.ContentType = contentType;
                data = Encoding.UTF8.GetBytes(provider.Invoke());
                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
                context.Response.Close();
            }
            catch (HttpListenerException) { }
        }
    }
}
