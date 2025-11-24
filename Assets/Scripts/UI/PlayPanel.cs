using UnityEngine;
using CoghillClan.PanelManager;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.Contracts;


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

    [SerializeReference] TextMeshProUGUI playerNamePrefab;

    [SerializeReference] TMP_Text headerText;
    [SerializeReference] TMP_Text gameNumberText;
    [SerializeReference] TMP_Text playerNameText;
    [SerializeReference] CanvasGroup playArea;
    [SerializeReference] TMP_InputField wordInput;
    [SerializeReference] TMP_Text rootWordText;
    [SerializeReference] Button clearBtn;
    [SerializeReference] Button sendBtn;
    [SerializeReference] CanvasGroup controlButtons;
    [SerializeReference] Button startBtn;
    [SerializeReference] Button quitBtn;
    [SerializeReference] RectTransform playerListPanel;

    bool IsPlayRunning = false;

    private const string k_WordTypeBefore = "B";
    private const string k_WordTypeAfter = "A";

    public override void OnPanelLoaded()
    {
        gameManager = GameManager.Instance;
        SetListeners();
    }

    private void SetListeners()
    {
        clearBtn.onClick.AddListener(OnClearBtnClicked);
        sendBtn.onClick.AddListener(OnSendBtnClicked);
        startBtn.onClick.AddListener(OnStartBtnClicked);
        quitBtn.onClick.AddListener(OnQuitBtnClicked);

        GameEvents.BeginPlay.AddListener(OnBeginPlay);
        GameEvents.UpdateHostsPlayerList.AddListener(OnUpdateHostsPlayerList);
    }



    public override void OnPanelEnabled()
    {
        /*
         * Set text areas.
         * Turn off the play input area.
         * Turn off control button area if not the host.
         */

        GameEvents.BeginPlay.AddListener(OnBeginPlay);
        gameNumberText.text = $"Connected to game #{gameManager.ConnectToHostNumber}";
        playerNameText.text = gameManager.PlayerName;
        playArea.alpha = 0f;
        controlButtons.alpha = (gameManager.WeAreHost) ? 1f : 0f;

    }

    public override void OnPanelDisabled()
    {
        GameEvents.BeginPlay.RemoveListener(OnBeginPlay);
    }

    public void OnBeginPlay()
    {
        /*
         * Display the word on the screen.
         * Arrange the word-text and the input-field according to GameManager.WordType
         */

        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> ");

        rootWordText.text = gameManager.WordInPlay;
        switch (gameManager.WordType)
        {
            case k_WordTypeBefore: wordInput.transform.SetSiblingIndex(0); break;
            case k_WordTypeAfter: rootWordText.transform.SetSiblingIndex(0); break;
        }
        playArea.alpha = 1f;
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

    private void OnClearBtnClicked()
    {
        /*
         * Just clear the input field
         * //FIXME Reset focus
         */

        wordInput.text = "";
        //FIXME Set focus here
    }

    private void OnSendBtnClicked()
    {

    }

    private void OnStartBtnClicked()
    {
        /*
         * Signal the GameManager that the play button was clicked
         */

        GameEvents.StartNewGame.Invoke();
    }

    private void OnQuitBtnClicked()
    {

    }
}
