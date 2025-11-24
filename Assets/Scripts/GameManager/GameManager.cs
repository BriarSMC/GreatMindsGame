using System;
using System.Reflection;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEditor;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using CoghillClan.PanelManager;
using System.ComponentModel;
using System.Collections.Generic;

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
 * 0.1.0    28-Oct-2025 Cloned from GreatMinds-RPC-Test
 *          28-Oct-2025 Refactored Awake and Start out of GameManager.cs
 *          28-Oct-2025 Refactored public methods out of GameManager.cs
 *          28-Oct-2025 Refactored RPC methods out of GameManager.cs
**/
public partial class GameManager : NetworkBehaviour
{
    /*
     * This is a partial class. This file has the name of the class, so we use 
     * it to hold all the common elements between the other partial class files.
     * Many data and other definitions.
     *
     * Other source files comprising this class are:
     *   GameManagerStartup.cs          - All the Unity object initialization methods
     *   GameManagerPublicMethods.cs    - All of our Public methods
     *   GameManagerRpcMethods.cs       - All of our RPC methods
     *   GameManagerPanic.cs            - Panic-Catastrophic error display
     */

    // GameManager is a persistent GameObject
    public static GameManager Instance;

    // References to game objects
    public Player Player;
    public NetworkManager networkManager;
    public PanelManager panelManager;

    // Public Properties

    /*
     * players contains information about each player who connects to the host/server.
     * The OnClientConnectedCallback(ulong clientId) method (PublicMethods.cs) will call
     * the AddPlayerName() method (RpcMethods.cs) to add players as they connect.
     */
    private Dictionary<ulong, string> players = new Dictionary<ulong, string>();
    private Dictionary<ulong, string> answers = new Dictionary<ulong, string>();

    /*
     * ClientId         Copy of NetworkManager.Singleton.LocalClientId
     * PlayerName       Name of this instance's player (Set by NamePanel)
     * OurIPAddress     This instance's IP address in nnn.nnn.nnn.nnn format
     * OurNodeNumber    The last nnn number in our IP address (used by the host)
     * ConnectToHostNumber Used by the client for connecting to the host (used by the clients)
     * WeAreHost        Used instead of IsHost
     * WeArePlayer      Used instead of IsClient
     * ClientType       1 = Host, 2 = Player
     */
    public ulong ClientId = 0;
    public string PlayerName;
    public string ConnectionType;
    public string OurIPAddress;
    public string OurNodeNumber; //FIXME Refactor to something reflecting the game or host number
    public string ConnectToHostNumber;
    public bool WeAreHost = false;
    public bool WeArePlayer = false;
    public int ClientType = 0;
    public string WordInPlay;
    public string WordType;

    public enum ClientTypes { host = 1, player = 2 }

    /*
     * All code should refer to individual UI panels by the Panels enum.
     * The Dictionary translates the enum values to strings for the PanelManager
     * package.
     */
    public enum Panels
    {
        splashPanel,
        namePanel,
        networkPanel,
        hostPanel,
        playPanel,
        resultsPanel,
        messagePanel,
    }

    public static readonly Dictionary<Panels, string> PanelNames =
        new Dictionary<Panels, string>
        {
            { Panels.splashPanel, "SplashPanel" },
            { Panels.namePanel, "NamePanel" },
            { Panels.networkPanel, "NetworkPanel" },
            { Panels.hostPanel, "HostPanel" },
            { Panels.playPanel, "PlayPanel" },
            { Panels.resultsPanel, "ResultsPanel" },
            { Panels.messagePanel, "MessagePanel" },
        };

    // Panic Codes
    private PanicCode panicCode;

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

    // Constants

    public const string k_PanicSceneName = "PanicScene";
    public const int k_GamePortNumber = 7777;

    public const string k_PanelManagerPath = "/UIManager/Canvas/PanelManager/";
}