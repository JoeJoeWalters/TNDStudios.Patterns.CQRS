# Use of CQRS Pattern / Azure Function App / MVVM To Create Robust Brokered Search System

Example of Azure Functions and UI built with CQRS principles, Azure Function Apps and MVVM Pattern with Vue.JS to ensure seperation of concerns. 

Example is a UI (Web or Mobile) issuing searches that are robustly fulfilled by Azure Functions and a brokering service and the relevant service can query the state of the searches running via the brokering service even if the UI has crashed in between. 

## Walk Through Video Of Design & Reasoning Behind The System

(Sat on a terrace in Tenerife at the time so some background noise)
<iframe width="560" height="315" src="https://www.youtube.com/embed/O2Q35mUsIB8" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>

Note: Unfortunately built without the use of a Service Bus which would have been better but needed the system to work locally with available emulators so built with blob triggers instead. ISearchBroker is fulfilled with LocalSearchBroker for now with this in mind but a ServiceBusBroker can be injected later as the system is built with Dependency Inversion / Injection in mind.

## Technologies And Techniques Used
- .Net Core 2.2
- Azure Function App with Azure Functions v2 (.Net Core)
- Azure Storage (Tables & Blob Containers) via Azure Local Storage Emulator
- Vue.JS for MVVM Pattern & JQuery for Javascript Shortcuts
- AJAX for connecting and responding to the API from Javascript
- Dependency Injection to inject appropriate brokering service and configuration
- Azure DevOps for CI (No deployment (CD))

## Project Site (Kanban)
I have set up a project site and a Sprint to complete the first pass of this work, click on the link below to view the Kanban board for the first Sprint.

<a href="https://github.com/TNDStudios/TNDStudios.Patterns.CQRS/projects/1">Project Site Link</a>

## CI / CD Pipelines
Click on the below link to view the pipeline that was built to build the project after every commit to master.

<a href="https://dev.azure.com/tndstudios/Builds/_build?definitionId=2&_a=summary">Azure DevOps Build Pipeline Link</a>

## Walk Through Video Of The Solution
