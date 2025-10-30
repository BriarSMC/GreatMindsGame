using Unity.Netcode;
using UnityEngine;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

/**
 *
 * Copyright © 2025 by Steven M. Coghill
 * This project is licensed under the MIT License.
 * A copy of the MIT License can be found in the 
 * accompanying LICENSE.txt file.
 **/
/** 
 * https://games.coghillclan.net/
 * 
 * https://www.github.com/BriarSMC/
 *
 * Version: 0.1.0
 * Version History
 * ----------------------------------------------------------------------------
 * nn.nn.nn dd-mmm-yyyy Comment 
 * 0.1.0    27-Oct-2025 Create based on 
 **/
[AddComponentMenu("Enable ActiveSceneSynchronization")]
[HelpURL("https://github.com/BriarSMC/GreatMindsGame/wiki/Player.cs-HelpURL-Page")]
public class Player : NetworkBehaviour
{
    private string _playerName = "Name Not Set";
    public string PlayerName { get { return _playerName; } set { _playerName = SetPlayerName(value); } }

    private ulong _clientId;
    public ulong ClientId { get { return _clientId; } }

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null) throw new Exception("Could not find GameManager object.");
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        _clientId = obj;
    }

    public override void OnNetworkSpawn()
    {
        gameManager.Player = this;
    }

    private string SetPlayerName(string value)
    {
        /*
         * Null or blank names are not allowed.
         * Strip any non-printable characters from the string.
         */
        if (String.IsNullOrEmpty(value)) return _playerName;

        return Regex.Replace(value, @"\p{C}", string.Empty);
    }

    public void SetClientId(ulong clientId)
    {
        _clientId = clientId;
    }
}
