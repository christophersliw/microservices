using AutoMapper;
using Candidate.Application.Contracts.Persistence;
using Candidate.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Candidate.Application.Functions.Candidates.Events;

public class CreateCandidateApplicationEventHandler : IRequestHandler<CreateCandidateApplicationEvent>
{
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCandidateApplicationEventHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public CreateCandidateApplicationEventHandler(IMapper mapper, ILogger<CreateCandidateApplicationEventHandler> logger, IServiceProvider serviceProvider)
    {
        _mapper = mapper;
        _logger = logger;
        _serviceProvider = serviceProvider;
    }
    
    public async Task<Unit> Handle(CreateCandidateApplicationEvent request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CreateCandidateApplicationEventHandler > Handle - start");
        var userOfferEntity = _mapper.Map<UserOffer>(request);
        userOfferEntity.ApplicationDate = DateTime.Now;

        //ze wzgledu na to ze ten handler jest wywolywany z poziomy backgrounservice ktory jest singletonem
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IAsyncUserOfferRepository asyncUserOfferRepository = scope.ServiceProvider.GetRequiredService<IAsyncUserOfferRepository>();

            await asyncUserOfferRepository.AddAsync(userOfferEntity);
        }

        return Unit.Value;
    }
}