using MediatR;

namespace Recruitment.Application.Functions;

public class GetOfferByIdQuery : IRequest<OfferViewModel>
{
    public Guid OfferId { get; set; }
}