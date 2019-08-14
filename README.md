# Use of CQRS Pattern / Azure Function App / MVVM To Create Robust Brokered Search System

Example of Azure Functions and UI built with CQRS principles, Azure Function Apps and MVVM Pattern with Vue.JS to ensure seperation of concerns. 

Example is a UI (Web or Mobile) issuing searches that are robustly fulfilled by Azure Functions and a brokering service and the relevant service can query the state of the searches running via the brokering service even if the UI has crashed in between. 

## Walk Through Video Of Design & Reasoning Behind The System

<iframe width="560" height="315" src="https://www.youtube.com/embed/O2Q35mUsIB8" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

Note: Unfortunately built without the use of a Service Bus which would have been better but needed the system to work locally with available emulators so built with blob triggers instead. ISearchBroker is fulfilled with LocalSearchBroker for now with this in mind but a ServiceBusBroker can be injected later as the system is built with Dependency Inversion / Injection in mind.

## Technologies And Techniques Used
