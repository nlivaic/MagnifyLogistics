# Logistics Application
    
## Basic design

* High level overview: https://excalidraw.com/#json=c9u5-Nu4Lq0OYEyyR7gtH,0QrSBDiK9z6nwsS0_Z2CJw
* Projects:
  * `Core` contains business logic and has no external dependencies. The idea was to go along with Clean Architecture principles.
  * `Core.Tests` has a few tests on how bookings are made. Method names are designed to be self-explanatory.


## Topics To Discuss

* Most of the assignment was evident from the requirements, but the relationship between shipment requests, offers and booking might be a bit imprecise. I presumed a booking can be created off of a shipment request directly (when carrier accepts the shipment request) or off of an offer (when shipper accepts a counteroffer made by the carrier).
* `ShipmentRequest.Ticks` is meant to guard against concurrent requests by two carriers accepting the same shipment request.