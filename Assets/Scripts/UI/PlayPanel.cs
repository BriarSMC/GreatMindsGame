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
 * Version: 0.0.0
 * Version History
 * ----------------------------------------------------------------------------
 * 0.1.0    29-Oct-2025 From scratch
 **/

public class PlayPanel : Panel
{
    /*
     * This the game play panel for everyone
     */

    GameManager gameManager;
    TextMeshProUGUI playerNamePrefab;

    TMP_Text headerText;
    TMP_Text playerNameText;
    TMP_InputField prefixInput;
    TMP_Text wordText;
    TMP_InputField postfixInput;
    Button clearBtn;
    Button sendBtn;
    Button quitBtn;
    RectTransform playerListPanel;

    bool IsPlayRunning = false;

    readonly string k_PlayAreaPath = $"{GameManager.k_PanelManagerPath}PlayPanel/PlayArea/";
    readonly string k_ButtonAreaPath = $"{GameManager.k_PanelManagerPath}PlayPanel/ButtonArea/";

    /* protected override  */
    public override void OnPanelLoaded()
    {
        LoadUIReferences();
        SetListeners();
    }

    private void LoadUIReferences()
    {
        gameManager = GameManager.Instance;
        GameObject obj = Resources.Load<GameObject>("Prefabs/ConnectedPlayerText");
        headerText = transform.Find("HeaderText").GetComponent<TextMeshProUGUI>();
        playerNamePrefab = Instantiate(obj).GetComponent<TextMeshProUGUI>();
        playerNameText = transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>();
        prefixInput = transform.Find($"PlayArea/PrefixInput").GetComponent<TMP_InputField>();
        wordText = transform.Find($"PlayArea/WordText").GetComponent<TextMeshProUGUI>();
        postfixInput = transform.Find($"PlayArea/PostfixInput").GetComponent<TMP_InputField>();
        clearBtn = transform.Find($"ButtonArea/ClearBtn").GetComponent<Button>();
        sendBtn = transform.Find($"ButtonArea/SendBtn").GetComponent<Button>();
        quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
        playerListPanel = transform.Find("PlayerListPanel").GetComponent<RectTransform>();
    }

    private void SetListeners()
    {
        GameEvents.PlayStarted.AddListener(PlayStartedFired);
        GameEvents.UpdateHostsPlayerList.AddListener(OnUpdateHostsPlayerList);
    }

    public override void OnPanelEnabled()
    {
        playerNameText.text = gameManager.PlayerName;
    }

    public override void OnPanelDisabled()
    {
        GameEvents.PlayStarted.RemoveListener(PlayStartedFired);
    }

    public void PlayStartedFired()
    {

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
