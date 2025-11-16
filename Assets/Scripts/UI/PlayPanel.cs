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
    TextMeshProUGUI playerNamePrefab;

    TMP_Text headerText;
    TMP_Text gameNumberText;
    TMP_Text playerNameText;
    CanvasGroup playArea;
    TMP_InputField prefixInput;
    TMP_Text wordText;
    TMP_InputField postfixInput;
    Button clearBtn;
    Button sendBtn;
    CanvasGroup controlButtons;
    Button startBtn;
    Button quitBtn;
    RectTransform playerListPanel;

    bool IsPlayRunning = false;


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
        gameNumberText = transform.Find("GameNumberText").GetComponent<TextMeshProUGUI>();
        playerNamePrefab = Instantiate(obj).GetComponent<TextMeshProUGUI>();
        playerNameText = transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>();
        playArea = transform.Find("PlayArea").GetComponent<CanvasGroup>();
        prefixInput = transform.Find($"PlayArea/PrefixInput").GetComponent<TMP_InputField>();
        wordText = transform.Find($"PlayArea/WordText").GetComponent<TextMeshProUGUI>();
        postfixInput = transform.Find($"PlayArea/PostfixInput").GetComponent<TMP_InputField>();
        clearBtn = transform.Find($"PlayArea/ButtonArea/ClearBtn").GetComponent<Button>();
        sendBtn = transform.Find($"PlayArea/ButtonArea/SendBtn").GetComponent<Button>();
        controlButtons = transform.Find($"ControlButtons").GetComponent<CanvasGroup>();
        startBtn = transform.Find("ControlButtons/StartBtn").GetComponent<Button>();
        quitBtn = transform.Find("ControlButtons/QuitBtn").GetComponent<Button>();
        playerListPanel = transform.Find("PlayerListPanel").GetComponent<RectTransform>();
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
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> ");

        wordText.text = gameManager.WordInPlay;
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
         * Just brute force clear both input fields.
         * No fancy logic figuring out which one to do.
         * //FIXME Or...Since we have to set the focus we have to figure out which field for that.
         */

        prefixInput.text = "";
        postfixInput.text = "";
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
