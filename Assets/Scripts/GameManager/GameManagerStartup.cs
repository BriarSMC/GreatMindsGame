using System;
using System.Reflection;
using UnityEngine;
using Unity.Netcode;
using CoghillClan.PanelManager;
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
 * 0.1.0    28-Oct-2025 Refactored Awake and Start out of GameManager.cs
 **/

public partial class GameManager : NetworkBehaviour
{
  void OnEnable()
  {
    SceneManager.sceneLoaded += OnSceneLoaded;
  }
  void Awake()
  {
    // If we exist, destroy this instance and return, otherwise set Instance
    // and tell Unity to never unload us.
    if (Instance != null)
    {
      Destroy(this);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(this.gameObject);

    // Look for various game objects and set our references accordingly
    panelManager = FindFirstObjectByType<PanelManager>();
    networkManager = FindFirstObjectByType<NetworkManager>();
    if (panelManager == null) Panic(PanicCode.NoNetworkManagerFound);
    if (networkManager == null) Panic(PanicCode.NoNetworkManagerFound);
  }

  void Start()
  {
    NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
    GetOurIPAddress();
    panelManager.ManagerEnable(true);
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> ");
    panelManager = FindFirstObjectByType<PanelManager>();
  }
}