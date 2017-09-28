using Nexosis.Api.Client.Model;
using System;
using System.Threading.Tasks;

namespace Nexosis.Api.Client
{
    public interface IModelClient
    {

        /// <summary>
        /// Gets a model
        /// </summary>
        /// <param name="id">The id of the model.</param>
        /// <exception cref="NexosisClientException">Thrown when 4xx or 5xx response is received from server, or errors in parsing the resposne.</exception>
        /// <returns>A <see cref="ModelSummary"/> with information about the model.</returns>
        Task<ModelSummary> Get(Guid id);
    }
}
