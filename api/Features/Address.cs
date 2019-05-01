using api.Features.House;

public class Address
{
    public int Id { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }

    public House House { get; set; }
    public int HouseId { get; set; }
}