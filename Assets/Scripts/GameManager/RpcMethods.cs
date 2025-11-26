using System;
using System.Reflection;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEditor;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Xml.Serialization;

/**
 *
 * Copyright © 2025 by Steven M. Coghill
 * This project is licensed under the MIT License.
 * A copy of the MIT License can be found in the 
 * accompanying LICENSE.txt file.
 **/
/** 
 * https://games.coghillclan.net/GreatMinds
 * 
 * https://www.github.com/BriarSMC/GreatMindsGame.git
 *
 * Version: 0.1.0
 * Version History
 * ----------------------------------------------------------------------------
 * 0.1.0    28-Oct-2025 Refactored RPC methods out of GameManager.cs
 **/

public partial class GameManager : NetworkBehaviour
{

  System.Random random = new System.Random();

  /**
   * Server RPCs
   **/

  [Rpc(SendTo.Server)]
  public void AddPlayerRpc(ulong clientId, string name)
  {
    /*
     * A client invokes this method when adds the game's information to the server.
     * The server does not "listen" for clients to connect. Each client must explicitly
     * tell the server when it wants to join the game.
     *
     * Right now we just add the client's ID and player name to server's data structure.
     * Tell the clients new copy the new data.
     */

    if (!IsServer) return; // Probably redundant since we are declared SendTo.Server, but ...

    players.Add(clientId, name);
    answers.Add(clientId, "");
    string xml = XML.DataToXML(players);
    SetNewPlayerListRpc(xml);
  }

  [Rpc(SendTo.Server)]
  public void SendPlayersWordRpc(ulong clientId, string word)
  {
    /*
     * Record the player's word in the answers dictionary
     * If all players have send a word, then signal that we have all of them.
     * This signal will send the answers dictionary to the players and
     * indicate the end of the round.
     */

    answers.Add(clientId, word);

    if (answers.Count == players.Count) GameEvents.ReceivedAllPlayersWords.Invoke();
  }

  /*
   * Client/Host RPCs
   */

  [Rpc(SendTo.ClientsAndHost)]
  public void SetNewPlayerListRpc(string xml)
  {
    /*
     * If we are the server, then just return cuz we maintain the data anyway.
     * Tell game new data is available
     */

    if (!IsServer) players = XML.XMLToData(xml);
    GameEvents.UpdateHostsPlayerList.Invoke();
  }

  [Rpc(SendTo.ClientsAndHost)]
  public void StartNewGameRpc(string word)
  {
    /*
     * The word send to us is the format:
     *    WORD,WORDTYPE
     *
     * WORD is the word in play. 
     * WORDTYPE is one of the following: A, B, E (after, before, either)
     *
     * A means we are looking for a word that follows the word in play. 
     * B means we are looking for a word that precedes the word in play.
     * E means we are looking for either of them.
     *
     * If the word type is E, then we randomly select whether to use A or B.
     */

    var values = word.Split(",");
    WordInPlay = values[0];
    WordType = values[1];
    if (WordType == "E") WordType = random.Next(2) == 0 ? "A" : "B";
    GameEvents.BeginPlay.Invoke();
  }

  [Rpc(SendTo.ClientsAndHost)]
  public void SendResultsToPlayersRpc(string xml)
  {
    /*
     * Server calls this when all players have sent an answer.
     * This also indicates to the players that the round is over.
     *
     * xml is an XML representation of the answers dictionary.
     * We decode it and store it in our copy of the answers dictionary.
     */

    answers = XML.XMLToData(xml);
    GameEvents.GameOver.Invoke();
  }
}