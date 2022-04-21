using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using MyJetTools.Grpc.Sdk.GrpcMetrics;
using ProtoBuf.Grpc.Client;

namespace MyJetTools.Sdk.Grpc;

public class GrpcClientFactory
{
    private readonly CallInvoker _channel;

    public GrpcClientFactory(string grpcServiceUrl)
    {
        AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
        var channel = GrpcChannel.ForAddress(grpcServiceUrl);
        _channel = channel.Intercept(new PrometheusMetricsInterceptor());
    }

    public TService CreateGrpcService<TService>() where TService : class
    {
        return _channel.CreateGrpcService<TService>();
    }
}