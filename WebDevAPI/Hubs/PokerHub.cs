using Microsoft.AspNetCore.SignalR;
using WebDevAPI.Db.Dto_s.SignalR;
using WebDevAPI.Db.Models;
using WebDevAPI.Db.Repositories;
using WebDevAPI.Logic.CardLogic;

namespace WebDevAPI.Hubs
{
    public class PokerHub: Hub
    {

        private static List<string> playerConnections = new List<string>();

        // Send the updated game state to all connected clients
        public async Task SendGameState(ActionDto action)
        {
            await Clients.All.SendAsync("ReceiveGameState", action);
        }

        public async Task HandlePlayerAction(ActionDto action)
        {
            // Process the player action and update the game state accordingly
            // ...

            // Send the updated game state to all connected clients
            
            await SendGameState(action);
        }


        public async Task JoinTable(JoinTableDto player)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, player.Id.ToString());
            playerConnections.Add(Context.ConnectionId);
            await Clients.All.SendAsync("PlayerJoined", player);
        }

        public async Task StartGame(ICollection<Player> players)
        {
            var deckOfCards = new DeckOfCards();
            deckOfCards.SetUpDeck();


            DealCards dealCards = new DealCards();
            List<PlayerHand> playerHands = (List<PlayerHand>)dealCards.Deal(players, deckOfCards.getDeck);
            for (int i = 0; i < playerHands.Count; i++)
            {
                await Clients.Client(playerConnections[i]).SendAsync("ReceiveCards", playerHands[i]);
            }

            //also return the tablecards?

            await Clients.All.SendAsync("GameStarted", "Game started");
        }

    }
}
