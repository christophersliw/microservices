using Candidate.Domain.Entities;

namespace Candidate.Persistence.EF.DummyData;

public class DummyUsers
{
    public static List<User> Get()
    {
        User u1 = new User()
        {
            FirstName = "Jacek", Surrname = "Zdanowicz", UserId = 1
        };

        User u2 = new User()
        {
            FirstName = "Jurek", Surrname = "Wielki", UserId = 2
        };

        List<User> userList = new List<User>();
        userList.Add(u1);
        userList.Add(u2);
        
        return userList;
    }
}