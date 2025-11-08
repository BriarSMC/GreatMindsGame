using Unity.Netcode;
using UnityEngine;
using System.Reflection;
using Unity.Netcode.Transports.UTP;
using System;

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

public partial class GameManager : NetworkBehaviour
{

    private void RegisterEvents()
    {
        EventManager.SplashScreenFinished.AddListener(OnSplashScreenFinished);
        EventManager.PlayerNameSet.AddListener(OnPlayerNameSet);
        EventManager.HostBtnClicked.AddListener(OnHostBtnClicked);
        EventManager.ConnectBtnClicked.AddListener(OnConnectBtnClicked);
        EventManager.QuitBtnClicked.AddListener(QuitGame);
    }

    private void OnSplashScreenFinished()
    {
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.namePanel]);
    }

    private void OnPlayerNameSet(string name)
    {
        Player.PlayerName = name;
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.networkPanel]);
    }

    private void OnHostBtnClicked()
    {
        WeAreHost = true;
        ClientType = (int)ClientTypes.host;
        if (!NetworkManager.Singleton.StartHost()) Panic(PanicCode.CouldNotStartHost);

        panelManager.Push(GameManager.PanelNames[GameManager.Panels.hostPanel]);
    }

    private void OnConnectBtnClicked(string host)
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> {host}/{OurIPAddress}");
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        if (transport == null) Panic(PanicCode.NetworkTransportNotFound);

        string[] ourIPAddr = OurIPAddress.Split(".");
        ourIPAddr[ourIPAddr.Length - 1] = host;
        string iPAddr = String.Join(".", ourIPAddr);
        transport.SetConnectionData(iPAddr, k_GamePortNumber);
        WeArePlayer = true;
        ClientType = (int)ClientTypes.player;
        ConnectToHostNumber = host;

        if (!NetworkManager.Singleton.StartClient()) Panic(PanicCode.CouldNotStartClient);

        panelManager.Push(GameManager.PanelNames[GameManager.Panels.playPanel]);
    }

}
