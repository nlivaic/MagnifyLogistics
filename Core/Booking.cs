using Common.Base;

namespace Core;
public class Booking : BaseEntity<Guid>
{
    public ShipmentRequest ShipmentRequest { get; private set; }
    public Guid ShipmentRequestId { get; private set; }
    public DateTime BookedOn { get; private set; }
    public Carrier Carrier { get; private set; }
    public Guid CarrierId { get; private set; }
    public Offer Offer { get; private set; }
    public Guid OfferId { get; private set; }
    public decimal Price { get; set; }

    public Booking(
        ShipmentRequest shipmentRequest,
        Carrier carrier) : base(Guid.NewGuid())
    {
        ShipmentRequest = shipmentRequest;
        ShipmentRequestId = shipmentRequest.Id;
        BookedOn = DateTime.UtcNow;
        Carrier = carrier;
        CarrierId = carrier.Id;
        Price = shipmentRequest.BudgetAmount;
    }
    
    public Booking(
        ShipmentRequest shipmentRequest,
        Carrier carrier,
        Offer offer) : this(shipmentRequest, carrier)
    {
        Offer = offer;
        OfferId = offer.Id;
        Price = offer.Price;
    }
}