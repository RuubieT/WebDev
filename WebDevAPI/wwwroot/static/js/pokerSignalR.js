// 'use strict';

// var connection = new signalR.HubConnectionBuilder().withUrl('/chatHub').build();

// connection.on('ReceiveMessage', function () {
//   console.log('received');
// });

// connection
//   .start()
//   .then(function () {
//     console.log('Succesfully connected');
//     newWindowLoadedOnClient();
//   })
//   .catch(function (err) {
//     return console.error(err.toString());
//   });

// function newWindowLoadedOnClient() {
//   connection.send('NewWindowLoaded');
// }
