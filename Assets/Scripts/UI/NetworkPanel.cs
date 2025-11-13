using UnityEngine;
using CoghillClan.PanelManager;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

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
 * 0.1.0    29-Oct-2025 From scratch  * 
 **/

public class NetworkPanel : Panel
{
    /*
     * This panel allows the user to select whether they want to host a game or join an existing game.
     *
     * The panel displays a HOST button and a JOIN button.
     * Clicking HOST simply fires off the HostBtnClicked event so the game logic can do what's needed.
     * Clicking the JOIN button allows the player to enter a game/node number and then connect to the game.
     *
     * The panel has a region for entering the game/node number. By default it is "turned off." We do this
     * by setting its alpha channel to zero (0). When the player clicks the JOIN button we set the alpha to one (1)
     * which displays an input field and a CONNECT button. The player enters the node number and clicks the CONNECT
     * button. If the input value is valid, then we fire off the ConnectBtnClicked event. Otherwise we display 
     * an error and let the player try again.
     */

    GameManager gameManager;
    Button hostBtn;
    Button joinBtn;
    CanvasGroup connectionGroup;
    TMP_InputField hostNumberInput;
    TMP_Text errorMessageText;
    Button connectBtn;

    bool inputFieldTookFocus = true;

    public override void OnPanelLoaded()
    {
        gameManager = GameManager.Instance;
        hostBtn = GameObject.Find("HostBtn").GetComponent<Button>();
        hostBtn.onClick.AddListener(() => GameEvents.HostBtnClicked.Invoke());
        joinBtn = GameObject.Find("JoinBtn").GetComponent<Button>();
        joinBtn.onClick.AddListener(() => { connectionGroup.alpha = 1f; });
        connectionGroup = transform.Find("ConnectionGroup").GetComponent<CanvasGroup>();
        connectionGroup.alpha = 0f;
        hostNumberInput = connectionGroup.transform.Find("HostNumberInput").GetComponent<TMP_InputField>();
        errorMessageText = connectionGroup.transform.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        errorMessageText.alpha = 0f;
        connectBtn = connectionGroup.transform.Find("ConnectBtn").GetComponent<Button>();

        connectBtn.onClick.AddListener(OnConnectBtnClicked);
    }

    void Update()
    {
        /*
         * This update routine controls player interaction with the input field.
         *
         * If the user enters invalid data into the field, then we want to:
         *      - Erase any previous input value
         *      - Turn off any error message displayed
         *
         * We did not want to go through the effort of creating GainFocus and LostFocus events 
         * for the field. So we do it programmatically by testing the condition of the input field 
         * each Update() cycle.
         *
         * inputFieldTookFocus starts off as true. 
         * 
         * If the input field is in focus, then we check to see if inputFieldTookFocus is true.
         * If so, that means this is the first time through the Update() cycle since it took focus.
         * So we turn off our flag, turn off the error message and clear the input field.
         * Otherwise, we set our flag to catch the next time the input field gains focus.
         * 
         */
        if (hostNumberInput.isFocused)
        {
            if (inputFieldTookFocus)
            {
                inputFieldTookFocus = false;
                errorMessageText.alpha = 0f;
                hostNumberInput.text = "";
            }
        }
        else
        {
            inputFieldTookFocus = true;
        }
    }

    private void OnConnectBtnClicked()
    {
        /*
         * Input field must be an integer between 1 and 254 inclusive.
         */

        int i;
        if (hostNumberInput.text.IsUnityNull()) { DisplayErrorMessage("Please enter a number."); return; }
        if (!int.TryParse(hostNumberInput.text, out i)) { DisplayErrorMessage("Please enter a number."); return; }
        if (i <= 0 || i > 254) { DisplayErrorMessage("Host number must be between 1 and 254."); return; }

        GameEvents.ConnectBtnClicked.Invoke(i.ToString());
    }

    private void DisplayErrorMessage(string s)
    {
        errorMessageText.alpha = 1f;
        errorMessageText.text = s;
    }

}