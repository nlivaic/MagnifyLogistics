# Logistics Application
    
## Basic design

* High level overview: https://excalidraw.com/#json=c9u5-Nu4Lq0OYEyyR7gtH,0QrSBDiK9z6nwsS0_Z2CJw
* Projects:
  * `Core` contains business logic and has no external dependencies. The idea was to go along with Clean Architecture principles.
  * `Core.Tests` has a few tests on how bookings are made. Method names are designed to be self-explanatory.

## Topics To Discuss

* Most of the assignment was evident from the requirements, but the relationship between shipment requests, offers and booking might be a bit imprecise. I presumed a booking can be created off of a shipment request directly (when carrier accepts the shipment request) or off of an offer (when shipper accepts a counteroffer made by the carrier).
* `ShipmentRequest.Ticks` is meant to guard against concurrent requests by two carriers accepting the same shipment request.

# Cloud Question

## Logic Apps

* Very clunky if you try to use it outside of the simple use cases. Tech lead had already created some stuff using Logic Apps and gently pushed me into using them for the problem at hand - reading some zip files dumped on an FTP server by a 3rd party provider, and then unzipping them to get to the JSON files and sending those files to our APIs. It turned out to be such a problematic approach that after about more than a week of trying I said I would rather just code it up because then we would a) be done sooner and b) have more control. It took about two days to code it up.

## Azure Functions

* Worked nicely, but in order to get the function triggered by an Azure Service Bus message I had to use the built-in triggers. The problem with the trigger is it tightly coupled you to Azure Service Bus, there was no way to use a 3rd party messaging queue. I wanted to use Mass Transit due to it giving you a lot out of the box. But because of the tight coupling I just could not get MassTransit to work the way I wanted it to and could not use the error queues and automatically built topics and subscriptions. It just made the whole thing awkward and I had to drop the MassTransit.
 