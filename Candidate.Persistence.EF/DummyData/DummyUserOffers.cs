using Candidate.Domain.Entities;

namespace Candidate.Persistence.EF.DummyData;

public class DummyUserOffers
{
    public static List<UserOffer> Get()
    {
        UserOffer uo1 = new UserOffer()
        {
            OfferId = 1, ApplicationDate = DateTime.Parse("2022-0-01"), UserId = 1, UserOfferId = Guid.NewGuid()
        };

        UserOffer uo2 = new UserOffer()
        {
            OfferId = 2, ApplicationDate = DateTime.Parse("2022-0-01"), UserId = 1, UserOfferId = Guid.NewGuid()
        };

        var applicationList = new List<UserOffer>
        {
            uo1,
            uo1
        };

        return applicationList;
    }
}