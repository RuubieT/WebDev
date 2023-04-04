export class SignalRService {
  _connection;
  connectionIsEstablished = false;

  constructor() {
    this.createConnection();
    // this.registerOnServerEvents();
    this.startConnection();
  }

  createConnection() {
    this._connection = new signalR.HubConnectionBuilder()
      .withUrl('/pokerHub')
      .build();
  }

  startConnection() {
    this._connection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        console.log('Hub connection started');
      })
      .catch((err) => {
        console.log('Error while establishing connection, retrying...');
        setTimeout(this.startConnection(), 5000);
      });
  }

  //   registerOnServerEvents(){
  //     this._connection.on('ReceiveMessage', (data) => {
  //       this.messageReceived.emit(data);
  //     });
  //   }
}
