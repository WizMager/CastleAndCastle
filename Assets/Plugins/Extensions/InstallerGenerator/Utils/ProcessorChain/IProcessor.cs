namespace Plugins.Extensions.InstallerGenerator.Utils.ProcessorChain
{
    public interface IProcessor<TReq, TResp> where TReq : IRequest where TResp : IResponse
    {
        void DoProcess(TReq request, ref TResp response, IProcessorChain<TReq, TResp> processorChain);
    }
}