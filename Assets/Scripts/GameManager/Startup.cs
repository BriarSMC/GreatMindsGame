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
 * 0.1.0    28-Oct-2025 Refactored Awake and Start out of GameManager.cs
 **/

public partial class GameManager : NetworkBehaviour
{
  void OnEnable()
  {
    /*
     * Register method for when a new scene is loaded.
     * Right now, should only be needed during a PANIC situation.
     */
    SceneManager.sceneLoaded += OnSceneLoaded;
  }

  void Awake()
  {
    /*
     * Set us up as a persistent game object
     */
    if (Instance != null)
    {
      Destroy(this);
      return;
    }

    Instance = this;
    DontDestroyOnLoad(this.gameObject);

    /*
     * Load up references to various objects the GameManager uses a lot.
     * PANIC if any of them can't be found.
     */
    panelManager = FindFirstObjectByType<PanelManager>();
    networkManager = FindFirstObjectByType<NetworkManager>();
    if (panelManager == null) Panic(PanicCode.NoNetworkManagerFound);
    if (networkManager == null) Panic(PanicCode.NoNetworkManagerFound);
  }

  void Start()
  {
    /*
     * Register all the events we listen for. (EventHandler.cs)  
     * Register method to listen for when clients connect to game.
     * Load our IP address information.
     * Turn on the PanelManager.
     */
    RegisterEvents();
    NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
    GetOurIPAddress();
    panelManager.ManagerEnable(true);
  }

  private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
  {
    /*
     * Whenever a new scene is loaded:
     * Find the PanelManager. 
     * We don't need the NetworkManager or any other references at this point.
     */
    panelManager = FindFirstObjectByType<PanelManager>();
  }

}