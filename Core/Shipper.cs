using Common.Base;

namespace Core;
public class Shipper : BaseEntity<Guid>
{
    public IEnumerable<ShipmentRequest> ShipmentRequests => _shipmentRequests;

    private readonly List<ShipmentRequest> _shipmentRequests = new List<ShipmentRequest>();

    public Shipper() : base(Guid.NewGuid())
    {
    }

    public ShipmentRequest CreateShipmentRequest(
        Address pickupAddress,
        Address destinationAddress,
        decimal budgetAmount,
        string additionalInformation)
    {
        var shipmentRequest = new ShipmentRequest(
            this,
            pickupAddress,
            destinationAddress,
            budgetAmount,
            additionalInformation);
        _shipmentRequests.Add(shipmentRequest);
        return shipmentRequest;
    }

    public Booking AcceptOffer(Offer offer) =>
        offer.Accept(this);

    public void RejectOffer(Offer offer) =>
        offer.Reject(this);
}
