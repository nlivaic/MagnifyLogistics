namespace Core;
public class Address
{
    public string City { get; private set; }
    public string State { get; private set; }
    public string ZipCode { get; private set; }

    public Address(string city, string state, string zipCode)
    {
        City = city;
        State = state;
        ZipCode = zipCode;
    }
}