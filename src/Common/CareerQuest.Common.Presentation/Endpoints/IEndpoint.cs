using Microsoft.AspNetCore.Routing;

namespace CareerQuest.Common.Presentation.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
