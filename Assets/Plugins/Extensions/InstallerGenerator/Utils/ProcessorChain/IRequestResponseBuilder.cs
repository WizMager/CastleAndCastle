namespace Plugins.Extensions.InstallerGenerator.Utils.ProcessorChain
{
    public interface IRequestResponseBuilder<out TReq, out TResp> where TReq : IRequest where TResp : IResponse
    {
        TReq BuildRequest();
        TResp BuildResponse();
    }
}