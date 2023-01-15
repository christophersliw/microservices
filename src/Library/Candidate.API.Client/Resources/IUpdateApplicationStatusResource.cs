using Candidate.API.Client.RequestContent;
using Candidate.API.Client.Responses;

namespace Candidate.API.Client.Resources;

public interface IUpdateApplicationStatusResource
{
    Task<UpdateApplicationStatusResponse> UpdateStatus(UpdateStatusRequest requestContent,
        CancellationToken cancellationToken);
}