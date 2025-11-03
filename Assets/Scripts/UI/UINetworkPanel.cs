using UnityEngine;
using CoghillClan.PanelManager;
using UnityEngine.UI;
// using UnityEngine.UIElements;
using System;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
public class UINetworkPanel : Panel
{
    GameManager gameManager;
    // PanelManager panelManager;
    Button hostBtn;
    Button joinBtn;
    GameObject connectionGroup;
    TMP_InputField hostNumberInput;
    TMP_Text errorMessageText;
    Button connectBtn;

    bool inputFieldTookFocus = true;

    public override void OnPanelLoaded()
    {
        gameManager = GameManager.Instance;
        hostBtn = GameObject.Find("HostBtn").GetComponent<Button>();
        hostBtn.onClick.AddListener(HostBtnClicked);
        joinBtn = GameObject.Find("JoinBtn").GetComponent<Button>();
        joinBtn.onClick.AddListener(JoinBtnClicked);
        connectionGroup = transform.Find("ConnectionGroup").gameObject;
        connectionGroup.SetActive(false);
        hostNumberInput = connectionGroup.transform.Find("HostNumberInput").GetComponent<TMP_InputField>();
        errorMessageText = connectionGroup.transform.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        errorMessageText.alpha = 0f;
        connectBtn = connectionGroup.transform.Find("ConnectBtn").GetComponent<Button>();
        connectBtn.onClick.AddListener(ConnectBtnClicked);
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

    private void HostBtnClicked()
    {
        gameManager.SetWeAreHost();
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.hostPanel]);
    }

    private void JoinBtnClicked()
    {
        connectionGroup.SetActive(true);
    }

    private void ConnectBtnClicked()
    {
        int i;
        if (hostNumberInput.text.IsUnityNull()) { DisplayErrorMessage("Please enter a number."); return; }
        if (!int.TryParse(hostNumberInput.text, out i)) { DisplayErrorMessage("Please enter a number."); return; }
        if (i <= 0 || i > 254) { DisplayErrorMessage("Host number must be between 1 and 254."); return; }

        gameManager.SetWeArePlayer(i);
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.playPanel]);
    }

    private void DisplayErrorMessage(string s)
    {
        errorMessageText.alpha = 1f;
        errorMessageText.text = s;
    }

}