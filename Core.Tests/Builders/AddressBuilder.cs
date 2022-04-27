namespace Core.Tests.Builders;

class AddressBuilder
{
    public Address CreateAddress1() =>
        new Address("City1", "State1", "10000");
        
    public Address CreateAddress2() =>
        new Address("City2", "State2", "20000");

}