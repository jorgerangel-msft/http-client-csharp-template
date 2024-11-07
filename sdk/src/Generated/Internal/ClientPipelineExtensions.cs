// <auto-generated/>

#nullable disable

using System.ClientModel;
using System.ClientModel.Primitives;
using System.Threading.Tasks;

namespace SimplePost
{
    internal static partial class ClientPipelineExtensions
    {
        public static async ValueTask<PipelineResponse> ProcessMessageAsync(this ClientPipeline pipeline, PipelineMessage message, RequestOptions options)
        {
            await pipeline.SendAsync(message).ConfigureAwait(false);

            if (message.Response.IsError && (options?.ErrorOptions & ClientErrorBehaviors.NoThrow) != ClientErrorBehaviors.NoThrow)
            {
                throw await ClientResultException.CreateAsync(message.Response).ConfigureAwait(false);
            }

            PipelineResponse response = message.BufferResponse ? message.Response : ExtractResponseContent(message);
            return response;
        }

        public static PipelineResponse ProcessMessage(this ClientPipeline pipeline, PipelineMessage message, RequestOptions options)
        {
            pipeline.Send(message);

            if (message.Response.IsError && (options?.ErrorOptions & ClientErrorBehaviors.NoThrow) != ClientErrorBehaviors.NoThrow)
            {
                throw new ClientResultException(message.Response);
            }

            PipelineResponse response = message.BufferResponse ? message.Response : ExtractResponseContent(message);
            return response;
        }

        public static async ValueTask<ClientResult<bool>> ProcessHeadAsBoolMessageAsync(this ClientPipeline pipeline, PipelineMessage message, RequestOptions options)
        {
            PipelineResponse response = await pipeline.ProcessMessageAsync(message, options).ConfigureAwait(false);
            switch (response.Status)
            {
                case >= 200 and < 300:
                    return ClientResult.FromValue(true, response);
                case >= 400 and < 500:
                    return ClientResult.FromValue(false, response);
                default:
                    return new ErrorResult<bool>(response, new ClientResultException(response));
            }
        }

        public static ClientResult<bool> ProcessHeadAsBoolMessage(this ClientPipeline pipeline, PipelineMessage message, RequestOptions options)
        {
            PipelineResponse response = pipeline.ProcessMessage(message, options);
            switch (response.Status)
            {
                case >= 200 and < 300:
                    return ClientResult.FromValue(true, response);
                case >= 400 and < 500:
                    return ClientResult.FromValue(false, response);
                default:
                    return new ErrorResult<bool>(response, new ClientResultException(response));
            }
        }

        private static PipelineResponse ExtractResponseContent(PipelineMessage message)
        {
            return message.ExtractResponse();
        }
    }
}