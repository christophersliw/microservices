using Candidate.Domain.Entities;

namespace Candidate.Persistence.EF.DummyData;

public class DummyUserOffers
{
    private static List<UserOffer> _applicationList;

    static DummyUserOffers()
    {
        UserOffer uo1 = new UserOffer()
        {
            OfferId = 1, ApplicationDate = DateTime.Parse("2022-01-01"), UserId = 1, UserOfferId = Guid.NewGuid()
        };

        UserOffer uo2 = new UserOffer()
        {
            OfferId = 2, ApplicationDate = DateTime.Parse("2022-01-01"), UserId = 1, UserOfferId = Guid.NewGuid()
        };

        _applicationList = new List<UserOffer>
        {
            uo1,
            uo2
        };
    }

    public static List<UserOffer> Get()
    {
        return _applicationList;
    }

    public static UserOffer Add(UserOffer userOffer)
    {
        userOffer.UserOfferId = Guid.NewGuid();
        _applicationList.Add(userOffer);

        return userOffer;
    }
}