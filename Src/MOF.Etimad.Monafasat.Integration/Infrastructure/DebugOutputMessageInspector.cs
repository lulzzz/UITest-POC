using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace MOF.Etimad.Monafasat.Integration.Infrastructure
{
    // Client message inspector  
    public class DebugOutputMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            Debug.WriteLine(request.ToString());
            return null;
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            Debug.WriteLine(reply.ToString());
        }

    }
    public class DebugOutputEndpointBehavior : IEndpointBehavior
    {
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
            // No implementation necessary  
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.ClientMessageInspectors.Add(new DebugOutputMessageInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            // No implementation necessary  
        }

        public void Validate(ServiceEndpoint endpoint)
        {
            // No implementation necessary  
        }
    }
}
