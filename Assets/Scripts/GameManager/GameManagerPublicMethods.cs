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
 * 0.1.0    28-Oct-2025 Refactored public methods out of GameManager.cs
 **/

public partial class GameManager : NetworkBehaviour
{
  private void GetOurIPAddress()
  {
    string hostName = Dns.GetHostName();
    Debug.Log($"Our Host Name: {hostName}");
    IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
    OurIPAddress = hostEntry.AddressList[0].ToString();
    Debug.Log($"Our IP Address: {OurIPAddress}");
    OurHostNumber = OurIPAddress.Split('.').Last();
  }

  public void OnClientConnectedCallback(ulong clientId)
  {
    if (clientId != NetworkManager.Singleton.LocalClientId) return;

    ClientId = clientId;
    Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> clientId: {clientId}/ClientId: {ClientId}");

    // AddPlayerRpc(ClientId, "NameNotSet");
    // SetHeaderText();
  }

  public void QuitGame()
  {
#if UNITY_EDITOR
    // This code will only run in the Unity Editor
    EditorApplication.isPlaying = false;
#else
        // This code will run in a built application (though it won't be called if ExitGameInEditor is only for editor)
        Application.Quit(); 
#endif
  }

  public void Panic(PanicCode code)
  {
    panicCode = code;
    SceneManager.LoadScene(k_PanicSceneName);
  }

  public int GetPanicCodeInt()
  {
    return (int)panicCode;
  }

}

