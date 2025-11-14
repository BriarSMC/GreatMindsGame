using UnityEngine;
using CoghillClan.PanelManager;
using TMPro;
using UnityEngine.UI;
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
 * 0.1.0    01-Nov-2025 From scratch
 **/

public class HostPanel : Panel
{
    /*
     * This panel lets the host start the game.
     * It displays:
     *      The Game Number (Host Node Number)
     *      List of connected players
     *      A START button
     *  
     * Host can start a new game only if two or more players are connected.
     */

    GameManager gameManager;
    TextMeshProUGUI playerNamePrefab;

    TextMeshProUGUI hostNumberText;
    Button startGameBtn;
    Button quitBtn;
    RectTransform playerListPanel;



    public override void OnPanelLoaded()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject obj = Resources.Load<GameObject>("Prefabs/ConnectedPlayerText");
        playerNamePrefab = Instantiate(obj).GetComponent<TextMeshProUGUI>();
        hostNumberText = transform.Find("HostNumberText").GetComponent<TextMeshProUGUI>();
        startGameBtn = transform.Find("StartGameBtn").GetComponent<Button>();
        startGameBtn.onClick.AddListener(OnStartGameBtnClicked);
        quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(gameManager.QuitGame);
        playerListPanel = transform.Find("PlayerListPanel").GetComponent<RectTransform>();

        GameEvents.UpdateHostsPlayerList.AddListener(OnUpdateHostsPlayerList);
    }

    public override void OnPanelEnabled()
    {
        hostNumberText.text = $"Game #{gameManager.OurNodeNumber}";
        OnUpdateHostsPlayerList();
    }

    private void OnStartGameBtnClicked()
    {
        /*
         * Don't start a game if the host is the only player connected
         */

        if (gameManager.GetPlayersCount() <= 1) return;
        GameEvents.PlayStarted.Invoke();
    }


    private void OnUpdateHostsPlayerList()
    {
        /*
         * Update the players list in the HostPanel
         */
        gameManager.DestroyAllChildren(playerListPanel.gameObject); //transform.Find("PlayerListPanel").gameObject);

        foreach (KeyValuePair<ulong, string> kvp in gameManager.GetPlayers())
        {
            string tmp = $"{kvp.Value} ({kvp.Key})";
            TextMeshProUGUI prefab = Instantiate(playerNamePrefab);
            prefab.text = tmp;
            prefab.transform.SetParent(playerListPanel, false);
        }
    }
}
