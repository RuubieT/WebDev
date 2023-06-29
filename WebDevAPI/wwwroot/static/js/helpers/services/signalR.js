import { assignPokertable, dealCards, tableCards } from '../gamelogic.js';

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
      .configureLogging(signalR.LogLevel.Information)
      .withAutomaticReconnect()
      .build();
  }

  handleGameState(gameState) {
    console.log(gameState);
  }

  handlePlayerJoin() {
    assignPokertable();
  }

  handleStartGame(tablecards) {
    tableCards(tablecards);
    alert('Game started');
  }

  handleReceiveCards(data) {
    //update UI

    dealCards(data);
    console.log('CARDS?');
  }

  startConnection() {
    this._connection
      .start()
      .then(() => {
        this.connectionIsEstablished = true;
        this._connection.on('ReceiveGameState', this.handleGameState);
        this._connection.on('PlayerJoined', this.handlePlayerJoin);
        this._connection.on('GameStarted', this.handleStartGame);
        this._connection.on('ReceiveCards', this.handleReceiveCards);
        console.log('Hub connection started');
      })
      .catch((err) => {
        console.log('Error while establishing connection');
        console.log(err);
        setTimeout(this.startConnection(), 5000);
      });
  }

  sendPlayerAction(action) {
    this._connection.invoke('HandlePlayerAction', action).catch((error) => {
      console.error(error);
    });
  }

  joinTable(player) {
    this._connection.invoke('JoinTable', player).catch((error) => {
      console.error(error);
    });
  }

  startGame(players) {
    this._connection.invoke('StartGame', players).catch((error) => {
      console.error(error);
    });
  }

  //   registerOnServerEvents(){
  //     this._connection.on('ReceiveMessage', (data) => {
  //       this.messageReceived.emit(data);
  //     });
  //   }
}
