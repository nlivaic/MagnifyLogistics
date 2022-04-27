using System;
using Common.Exceptions;
using Core.Tests.Builders;
using Xunit;

namespace Core.Tests;

public class ShipmentRequestTests
{
    [Fact]
    public void Carrier_AcceptsShipment_Successfully()
    {
        // Arrange
        var carrier = new Carrier();
        var shipper = new Shipper();
        var addressBuilder = new AddressBuilder();
        var target = new ShipmentRequest(
            shipper,
            addressBuilder.CreateAddress1(),
            addressBuilder.CreateAddress2(),
            100m,
            "info");

        // Act
        var result = carrier.BookShipment(target.Ticks, target);

        // Assert
        Assert.Equal(target.Id, result.ShipmentRequestId);
        Assert.True(result.BookedOn > DateTime.UtcNow.AddSeconds(-10));
        Assert.Equal(carrier.Id, result.CarrierId);
        Assert.Null(result.Offer);
        Assert.Equal(target.BudgetAmount, result.Price);
    }

    [Fact]
    public void Shipper_AcceptsCarriersOffer_Successfully()
    {
        // Arrange
        var carrier = new Carrier();
        var shipper = new Shipper();
        var addressBuilder = new AddressBuilder();
        var target = new ShipmentRequest(
            shipper,
            addressBuilder.CreateAddress1(),
            addressBuilder.CreateAddress2(),
            100m,
            "info");
        var offer = new Offer(carrier, target, 120m);

        // Act
        var result = shipper.AcceptOffer(offer);

        // Assert
        Assert.Equal(target.Id, result.ShipmentRequestId);
        Assert.True(result.BookedOn > DateTime.UtcNow.AddSeconds(-10));
        Assert.Equal(carrier.Id, result.CarrierId);
        Assert.Equal(offer.Id, result.OfferId);
        Assert.Equal(offer.Price, result.Price);
    }
    [Fact]
    public void TwoCarriers_AcceptShipment_FirstSuccessfully_SecondThrows()
    {
        // Arrange
        var carrierSuccessful = new Carrier();
        var carrierFails = new Carrier();
        var shipper = new Shipper();
        var addressBuilder = new AddressBuilder();
        var target = new ShipmentRequest(
            shipper,
            addressBuilder.CreateAddress1(),
            addressBuilder.CreateAddress2(),
            100m,
            "info");
        var originalTicks = target.Ticks;
        var successfulBooking = carrierSuccessful.BookShipment(originalTicks, target);

        // Act, Assert
        Assert.Throws<ConcurrentException>(() => carrierFails.BookShipment(originalTicks, target));
    }
}