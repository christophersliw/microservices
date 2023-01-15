using Recruitment.Domain.Enities;
namespace Recruitment.Persistence.EF.DummyData;
public class DummyOffer
{
    public static List<Offer> Get()
    {
        Offer o1 = new Offer()
        {
            Name = "Programista c#",
            OfferId = 1
        };

        Offer o2 = new Offer()
        {
            Name = "Programista angular",
            OfferId = 2
        };
        
        Offer o3 = new Offer()
        {
            Name = "Programista java",
            OfferId = 3
        };
        
        
        List<Offer> list = new List<Offer>();

        list.Add(o1);
        list.Add(o2);
        list.Add(o3);

        return list;
    }
}