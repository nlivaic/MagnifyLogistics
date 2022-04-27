using Common.Base;

namespace Core;
public class Carrier : BaseEntity<Guid>
{
    public IEnumerable<Offer> Offers => _offers;

    private readonly List<Offer> _offers = new List<Offer>();

    public Carrier() : base(Guid.NewGuid())
    {
    }

    public Booking BookShipment(long ticks, ShipmentRequest shipmentRequest) =>
        shipmentRequest.Book(ticks, this);

    public Offer CreateOffer(ShipmentRequest shipmentRequest, decimal price)
    {
        var offer = new Offer(this, shipmentRequest, price);
        _offers.Add(offer);
        return offer;
    }
}
