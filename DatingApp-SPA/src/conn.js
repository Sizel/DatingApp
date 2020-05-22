import * as signalR from "@aspnet/signalr";

debugger;
let connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5000/messagesHub")
  .build();

connection.start().catch(error => {
    return console.error(error.toString());
});

// connection.on("ReceiveMessage", message => {
//   console.log(message);
// });

// let userId = '1';
// connection
//   .invoke('SendMessageToUser', userId, 'Test message')
//   .catch((error) => {
//     console.error(error.toString());
//   });