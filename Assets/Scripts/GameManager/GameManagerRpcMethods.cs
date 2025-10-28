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
// DELETEME Delete the following line before first rc commit
 * nn.nn.nn dd-mmm-yyyy Comment 
 * 0.1.0    28-Oct-2025 Refactored RPC methods out of GameManager.cs
 **/

public partial class GameManager : NetworkBehaviour
{
  // [Rpc(SendTo.Server)]
  // public void AddPlayerRpc(ulong clientId, string name)
  // {
  //     if (!IsServer) return;

  //     Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> Adding ID: {clientId}  Name: {name}");
  //     playerData.Add(clientId, name);
  //     Debug.Log(playerData.Count);
  //     AppendToOutput($"{clientId} connected.");
  // }

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