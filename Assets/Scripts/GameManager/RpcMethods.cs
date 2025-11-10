using System;
using System.Reflection;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEditor;
using Unity.Netcode;
using UnityEngine.SceneManagement;

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
     */

    if (!IsServer) return; // Probably redundant since we are declared SendTo.Server, but ...

    players.Add(clientId, name);

    // Send new list to all clients here
  }

  /*
  * Host RPCs
  * There are no host specific RPCs because the host acts as the server too.
  */

  /*
   * Client RPCs
   */




  // [Rpc(SendTo.Server)]
  // public void SetPlayerNameRpc(ulong clientId, string name)
  // {
  //     if (!IsServer) return;

  //     playerData[clientId] = name;

  //     AppendToOutput($"{clientId} name changed to {name}");

  //     Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> New Name set from: {clientId}  Name: {name}");
  //     Debug.Log($"playerData contains:");
  //     foreach (var kvp in playerData)
  //     {
  //         Debug.Log($"ID: {kvp.Key}  Name: {kvp.Value}");
  //     }
  // }

  // [Rpc(SendTo.Server)]
  // public void RequestDictionaryRpc()
  // {
  //     string output = "";
  //     foreach (var kvp in playerData)
  //     {
  //         output += $"ID:{kvp.Key} Name:{kvp.Value}\n";
  //     }
  //     SendDictionaryRpc(output);
  // }

  // [Rpc(SendTo.ClientsAndHost)]
  // public void SendDictionaryRpc(string s)
  // {
  //     AppendToOutput(s);
  // }
}