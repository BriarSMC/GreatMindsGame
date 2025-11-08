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
        hostBtn.onClick.AddListener(() => EventManager.HostBtnClicked.Invoke());
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
        int i;
        if (hostNumberInput.text.IsUnityNull()) { DisplayErrorMessage("Please enter a number."); return; }
        if (!int.TryParse(hostNumberInput.text, out i)) { DisplayErrorMessage("Please enter a number."); return; }
        if (i <= 0 || i > 254) { DisplayErrorMessage("Host number must be between 1 and 254."); return; }

        EventManager.ConnectBtnClicked.Invoke(i.ToString());
    }

    private void DisplayErrorMessage(string s)
    {
        errorMessageText.alpha = 1f;
        errorMessageText.text = s;
    }

}