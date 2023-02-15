# ServiceBusTest

## First:
1) Run Nuget restore on the application and make sure it builds.
2) Before running this code, you will need to go into "DomainModels/AppConfiguration.cs" and set the 3 variables with the correct vales for your ServiceBus account.
  - **SubcriptionName_OfClient** // This is used when you try to read messages
  - **TopicName** // the topic on which the messages are published
  - **ConnectionString** // Connection string to the ServiceBus Topic
  
 ## Sending messages:
 Run the ServiceBusTest project which is a web api. 
 It will run on localhost:5183 by default
 Then you can use your browser to view the Swagger page at http://localhost:5183/swagger/index.html
 
 In the example below, I will test it with 2 messages in the body
 
![image](https://user-images.githubusercontent.com/13032350/219119009-452500ba-af40-4336-b51e-db339f061a9f.png)


 ## Receiving messages:
 Run the "ServiceBusTopicClient" project. It is a console app so you can watch the console while it is running and see it recieve and write the 2 messages that we sent.

![image](https://user-images.githubusercontent.com/13032350/219119460-d2fde233-87e5-416c-a6ce-4e81c65f587c.png)

 
 
