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
 * 0.1.0    28-Oct-2025 Cloned from GreatMinds-RPC-Test
 *          28-Oct-2025 Refactored Awake and Start out of GameManager.cs
 *          28-Oct-2025 Refactored public methods out of GameManager.cs
 *          28-Oct-2025 Refactored RPC methods out of GameManager.cs
**/
public partial class GameManager : NetworkBehaviour
{
    // GameManager is a persistent GameObject
    public static GameManager Instance;

    // References to game objects
    public Player Player;
    private NetworkManager networkManager;

    // Public Properties
    public ulong ClientId;
    public string ConnectionType;
    public string OurIPAddress;
    public string OurHostNumber;
    public int PanicCode;

    // Private Properties

    // Constants

    const string k_PanicSceneName = "PanicScene";

    // Enums

    /*
     * This is a partial class. This file has the name of the class, so we use 
     * it to hold all the common elements between the other partial class files.
     * Many data and other definitions.
     *
     * Other source files comprising this class are:
     *   GameManagerStartup.cs          - All the Unity object initialization methods
     *   GameManagerPublicMethods.cs    - All of our Public methods
     *   GameManagerRpcMethods.cs       - All of our RPC methods
     */



}