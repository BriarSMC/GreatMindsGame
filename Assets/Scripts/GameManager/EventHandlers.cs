using Unity.Netcode;
using UnityEngine;
using System.Reflection;
using Unity.Netcode.Transports.UTP;
using System;
using System.Collections.Generic;
using System.Linq;

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
 * 0.1.0    03-Nov-2025 From scratch
 **/

/*
 * GreatMinds is an event driven game. GreatMinds does not use Update(), etc. for
 * game control. UI sometimes uses Update(), etc. for player input control, formatting, etc.
 *
 * This module defines and registers all the event subscriptions used during game play.
 * GameEvents.cs defines all of the events.
 */
public partial class GameManager : NetworkBehaviour
{

    private void RegisterEvents()
    {
        /*
         * Register the events this modules handles.
         */
        GameEvents.SplashScreenFinished.AddListener(OnSplashScreenFinished);
        GameEvents.PlayerNameSet.AddListener(OnPlayerNameSet);
        GameEvents.HostBtnClicked.AddListener(OnHostBtnClicked);
        GameEvents.ConnectBtnClicked.AddListener(OnConnectBtnClicked);
        GameEvents.NewPlayerListAvailable.AddListener(OnNewPlayerListAvailable);
        GameEvents.StartNewGame.AddListener(OnStartNewGame);

        GameEvents.QuitBtnClicked.AddListener(QuitGame);
    }

    private void OnSplashScreenFinished()
    {
        /*
         * The SplashPanel has an animation track attached to it so that
         * when its timer expires, this method is invoked.
         * All we do is display the next panel.
         */
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.namePanel]);
    }

    private void OnPlayerNameSet(string name)
    {
        /*
         * The UI lets the player set their name. The UI will invoke this method to save
         * the player's name when the player clicks the associated button.
         * We then display the next panel.
         */
        PlayerName = name;
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.networkPanel]);
    }

    private void OnHostBtnClicked()
    {
        /*
         * The player has clicked the HOST button in the UI.
         * Set that we are a the host.
         * Try to start ourselves as a host. If it fails, then do a PANIC stop.
         * Save this instance's client ID.
         * Display the next panel.
         */
        WeAreHost = true;
        ClientType = (int)ClientTypes.host;
        if (!NetworkManager.Singleton.StartHost()) Panic(PanicCode.CouldNotStartHost);
        ClientId = NetworkManager.Singleton.LocalClientId;
        ConnectToHostNumber = OurNodeNumber;

        panelManager.Push(GameManager.PanelNames[GameManager.Panels.playPanel]);
    }

    private void OnConnectBtnClicked(string host)
    {
        /*
         * The player has clicked the CONNECT button in the UI to join the host as a client.
         * Find the transport. Do a PANIC stop if not found.
         * When the HOST starts a game, the game displays the last number of the IP address.
         * We use this as the "game number." GameManager finds this value has startup.
         * The player enters this number and we use it to set the UnityTransport to communicate
         * with the host.
         * //DELETEME NOTE: We've disabled this for debugging purposes on a the development machine.
         * //DELETEME       So uncomment the SetConnectionData call for beta testing
         *
         * Set that we are a player and what host we are connecting to.
         * Start us as a client. If it fails, then do a PANIC stop.
         * Save our LocalClientId.
         * Display the next panel.
         */
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport == null) Panic(PanicCode.NetworkTransportNotFound);

        string[] ourIPAddr = OurIPAddress.Split(".");
        ourIPAddr[ourIPAddr.Length - 1] = host;
        string iPAddr = String.Join(".", ourIPAddr);
        //FIXME transport.SetConnectionData(iPAddr, k_GamePortNumber);
        WeArePlayer = true;
        ClientType = (int)ClientTypes.player;
        ConnectToHostNumber = host;

        if (!NetworkManager.Singleton.StartClient()) Panic(PanicCode.CouldNotStartClient);
        ClientId = NetworkManager.Singleton.LocalClientId;


        panelManager.Push(GameManager.PanelNames[GameManager.Panels.playPanel]);
    }

    private void OnNewPlayerListAvailable(Dictionary<ulong, string> newDictionary)
    {
        /*
         * The players data has been updated.
         * If we are the server, then just ignore cuz the server maintains the data.
         * Copy the new data to our instance's data.
         * Now trigger to update players data wherever needed.
         */
        players = newDictionary.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        GameEvents.UpdateHostsPlayerList.Invoke();
    }

    private void OnStartNewGame()
    {
        /*
         * Get a new word
         * Tell all the players that play as started
         */

        string playWord = "FOOBAR"; //FIXME Change to getting a real word later
        StartNewGameRpc(playWord);
    }
}
