using Common.Base;
using Common.Exceptions;

namespace Core;
public class ShipmentRequest : BaseEntity<Guid>, IConcurrent
{
    public Shipper Shipper { get; private set; }
    public Guid ShipperId { get; private set; }
    public Address PickupAddress { get; private set; }
    public Address DestinationAddress { get; private set; }
    public decimal BudgetAmount { get; private set; }
    public string AdditionalInformation { get; private set; }
    public Booking Booking { get; private set; }
    public long Ticks { get; private set; }

    public ShipmentRequest(
        Shipper shipper,
        Address pickupAddress,
        Address destinationAddress,
        decimal budgetAmount,
        string additionalInformation) : base(Guid.NewGuid())
    {
        Shipper = shipper;
        PickupAddress = pickupAddress;
        DestinationAddress = destinationAddress;
        BudgetAmount = budgetAmount;
        AdditionalInformation = additionalInformation;
        Ticks = DateTime.UtcNow.Ticks;
    }

    // To be called by the carrier when booking the shipment request directly.
    // Ticks are there to prevent concurrent bookings from different carriers.
    public Booking Book(long ticks, Carrier carrier)
    {
        if (ticks < Ticks)
        {
            throw new ConcurrentException($"Cannot book shipment request {Id}. User operating on stale version.");
        }
        Ticks = DateTime.UtcNow.Ticks;
        Booking = new Booking(this, carrier);
        return Booking;
    }

    // To be called by the shipper when booking the shipment request 
    // by accepting an offer.
    public Booking Book(Offer offer)
    {
        Ticks = DateTime.UtcNow.Ticks;
        Booking = new Booking(this, offer.Carrier, offer);
        return Booking;
    }
}