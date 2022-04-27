using Common.Base;

namespace Core;
public class Offer : BaseEntity<Guid>
{
    public Carrier Carrier { get; private set; }
    public Guid CarrierId { get; private set; }
    public ShipmentRequest ShipmentRequest { get; private set; }
    public Guid ShipmentRequestId { get; private set; }
    public decimal Price { get; private set; }
    public OfferStatus Status { get; private set; }

    public Offer(
        Carrier carrier,
        ShipmentRequest shipmentRequest,
        decimal price) : base(Guid.NewGuid())
    {
        if (price < 0m)
        {
            throw new Exception("Offered price cannot be less than 0.");
        }

        Carrier = carrier;
        ShipmentRequest = shipmentRequest;
        Price = price;
    }

    public Booking Accept(Shipper shipper)
    {
        if (ShipmentRequest.Shipper.Id != shipper.Id)
        {
            throw new ArgumentException("Offer can be accepted by the shipper owning the shipment request.");
        }
        if (Status == OfferStatus.Rejected)
        {
            throw new Exception("Cannot accept an offer that has already been rejected.");
        }
        Status = OfferStatus.Accepted;
        return ShipmentRequest.Book(this);
    }
    
    public void Reject(Shipper shipper)
    {
        if (ShipmentRequest.Shipper.Id != shipper.Id)
        {
            throw new ArgumentException("Offer can be rejected by the shipper owning the shipment request.");
        }
        if (Status == OfferStatus.Accepted)
        {
            throw new Exception("Cannot reject an offer that has already been accepted.");
        }
        Status = OfferStatus.Rejected;
    }
}