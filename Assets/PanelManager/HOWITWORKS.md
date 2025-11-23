# How It Works

_15-Nov-2025_

## Initiating Play

All Clients wait for the Host player to click the **START** button. The following takes place.

1. The Server selects a new word for play.
2. Send the new word to all of the Clients.
3. Signal that a new game has started.

### Mechanism

1. The Host player clicks the START button on the PlayPanel (the app displays the START button only on the Host's PlayPanel).
2. The START button's click event does:
   - Choose a new word
   - Invoke the StartNewGame event
3. The GameManager EventHandler listens for this event. It does the following upon receiving it:
   - Get a new word for play.
   - Call StartNewGameRpc with the new word
4. StartNewGameRpc(string) runs on all Clients (including the Host). It performs the following:
   - Stores the string parameter in a GameManager public variable (WordInPlay) so everyone can access it.
   - Invokes the BeginPlay event. (The PlayPanel subscribes to this event.)
5. PlayPanel intercepts the event.
   - Display the WordInPlay on the PlayPanel
   - Turn the PlayArea on so player can see the word.
   - Enable controls.
6. Wait for player to enter a word.
   - Player enters their word.
   - Player clicks send button
   - Send button calls SendPlayersWordRpc().
   - Player blocks until it receives results.
7. Server receives SendPlayerWordRpc().
   - Records the player's word.
   - Decrements the playersRemaining count.
   - When count hits zero (0), then it invokes the GameEvent.AllWordsSent event.
8. Host responds to GameEvent.AllWordsSent.
   - Since the host and the server are the same device, the host is the only instance that will respond to this event. The players data dictionaries should be filled with everyone's answers.
   - Host calls GameOverRpc() with the results.
9. Players respond to GameOverRpc()
   - Player's instance displays the results of the game.
   - Player's instance waits for the BeginPlay
   - Host instance give the option to start a new game
   - If clicked, then it invokes GameEvent.StartNewGame
