using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

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
 * Version: 0.0.0
 * Version History
 * ----------------------------------------------------------------------------
 * 0.1.0    28-Oct-2025 From scratch
 **/

public partial class GameManager : NetworkBehaviour
{

  // Panic Codes
  public enum PanicCode
  {
    NoGameManagerFound,
    NoNetworkManagerFound,
    CouldNotStartHost,
    CouldNotStartClient,
    NetworkTransportNotFound,
  }


  // Panic Code Messages
  public static readonly Dictionary<int, string> PanicMessageText = new Dictionary<int, string>()
  {
    {(int) PanicCode.NoGameManagerFound, "Could not find the GameManager." },
    {(int) PanicCode.NoNetworkManagerFound, "Could not find the NetworkManager."},
    {(int) PanicCode.CouldNotStartHost, "Could not start as a Host."},
    {(int) PanicCode.CouldNotStartClient, "Could not start as a Client."},
    {(int) PanicCode.NetworkTransportNotFound, "Could not find the Network Transport."},
  };
}
