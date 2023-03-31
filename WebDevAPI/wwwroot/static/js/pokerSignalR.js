'use strict';

var connection = new signalR.HubConnectionBuilder()
  .withUrl('/pokerHub')
  .build();

connection
  .start()
  .then(function () {
    console.log('Succesfully connected');
  })
  .catch(function (err) {
    return console.error(err.toString());
  });
