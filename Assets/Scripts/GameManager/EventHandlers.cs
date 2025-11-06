using Unity.Netcode;
using UnityEngine;

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
        EventManager.JoinBtnClicked.AddListener(OnJoinBtnClicked);
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
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.hostPanel]);
    }

    private void OnJoinBtnClicked(string host)
    {
        WeArePlayer = true;
        ClientType = (int)ClientTypes.player;
        ConnectToHostNumber = host;
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.playPanel]);
    }

}
