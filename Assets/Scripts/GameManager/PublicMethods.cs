using System;
using System.Reflection;
using System.Linq;
using System.Net;
using System.ComponentModel;
using UnityEngine;
using UnityEditor;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using CoghillClan.PanelManager;

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
 * 0.1.0    28-Oct-2025 Refactored public methods out of GameManager.cs
 **/

public partial class GameManager : NetworkBehaviour
{
  private void GetOurIPAddress()
  {
    /*
     * Extract our IP address and the "HOST number" from .NET
     */
    string hostName = Dns.GetHostName();
    IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
    OurIPAddress = hostEntry.AddressList[0].ToString();
    OurNodeNumber = OurIPAddress.Split('.').Last();
  }

  public void OnClientConnectedCallback(ulong clientId)
  {
    /*
     * Unity invokes this method when a client connects to host. (Registered in Start.cs)
     * Unity invokes this for both the server and client. We only want the client to execute
     * this code. The clients must explicitly call RPC methods to interact with the server.
     * 
     * Add our client ID and player name to the server's data structures.
     */
    if (clientId != NetworkManager.Singleton.LocalClientId) return; // Only continue if it's the client

    AddPlayerRpc(clientId, PlayerName);
  }

  public void Panic(PanicCode code)
  {
    /*
     * We invoke this routine whenever some catastrophic event happens. Something
     * totally unexpected and we can't recover from it. 
     *
     * We store the panic code passed to us in the GameManager so the next scene
     * can read it. Since the GameManager is persistent the next scene can retrieve 
     * it and display the associated message.
     */
    panicCode = code;
    SceneManager.LoadScene(k_PanicSceneName);
  }

  public int GetPanicCodeInt()
  {
    /*
     * Return the panic code enum as an integer
     */
    return (int)panicCode;
  }
}