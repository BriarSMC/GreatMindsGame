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
