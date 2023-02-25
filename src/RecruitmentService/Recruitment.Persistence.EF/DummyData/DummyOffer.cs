using Recruitment.Domain.Enities;
namespace Recruitment.Persistence.EF.DummyData;
public class DummyOffer
{
    public static List<Offer> Get()
    {
        Offer o1 = new Offer()
        {
            Name = "Programista c#",
            Id = Guid.NewGuid()
        };

        Offer o2 = new Offer()
        {
            Name = "Programista angular",
            Id = Guid.NewGuid()
        };
        
        Offer o3 = new Offer()
        {
            Name = "Programista java",
            Id = Guid.NewGuid()
        };
        
        
        List<Offer> list = new List<Offer>();

        list.Add(o1);
        list.Add(o2);
        list.Add(o3);

        return list;
    }
}