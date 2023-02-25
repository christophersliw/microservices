using Candidate.Domain.Entities;

namespace Candidate.Persistence.EF.DummyData;

public class DummyUsers
{
    public static List<User> Get()
    {
        User u1 = new User()
        {
            FirstName = "Jacek", Surrname = "Zdanowicz", Id = Guid.NewGuid()
        };

        User u2 = new User()
        {
            FirstName = "Jurek", Surrname = "Wielki",Id = Guid.NewGuid()
        };

        List<User> userList = new List<User>();
        userList.Add(u1);
        userList.Add(u2);
        
        return userList;
    }
}