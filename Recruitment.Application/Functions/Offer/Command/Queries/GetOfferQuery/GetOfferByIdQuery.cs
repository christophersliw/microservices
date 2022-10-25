using MediatR;

namespace Recruitment.Application.Functions;

public class GetOfferByIdQuery : IRequest<OfferViewModel>
{
    public int OfferId { get; set; }
}