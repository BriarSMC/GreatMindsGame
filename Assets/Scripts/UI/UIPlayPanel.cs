using UnityEngine;
using CoghillClan.PanelManager;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
public class UIPlayPanel : Panel
{
    GameManager gameManager;
    // PanelManager panelManager;
    TMP_Text headerText;
    TMP_Text playerNameText;
    TMP_InputField prefixInput;
    TMP_Text wordText;
    TMP_InputField postfixInput;
    Button clearBtn;
    Button sendBtn;
    Button quitBtn;


    bool IsPlayRunning = false;

    readonly string k_PlayAreaPath = $"{GameManager.k_PanelManagerPath}PlayPanel/PlayArea/";
    readonly string k_ButtonAreaPath = $"{GameManager.k_PanelManagerPath}PlayPanel/ButtonArea/";

    /* protected override  */
    public override void OnPanelLoaded()
    {
        LoadUIReferences();
    }

    private void LoadUIReferences()
    {
        gameManager = GameManager.Instance;
        headerText = transform.Find("HeaderText").GetComponent<TextMeshProUGUI>();
        playerNameText = transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>();
        prefixInput = transform.Find($"{k_PlayAreaPath}PrefixInput").GetComponent<TMP_InputField>();
        wordText = transform.Find($"{k_PlayAreaPath}WordText").GetComponent<TextMeshProUGUI>();
        postfixInput = transform.Find($"{k_PlayAreaPath}PostfixInput").GetComponent<TMP_InputField>();
        clearBtn = transform.Find($"{k_ButtonAreaPath}ClearBtn").GetComponent<Button>();
        sendBtn = transform.Find($"{k_ButtonAreaPath}SendBtn").GetComponent<Button>();
        quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
    }

    public override void OnPanelEnabled()
    {
        EventManager.Instance.PlayStarted += PlayStartedFired;
        playerNameText.text = gameManager.Player.PlayerName;
    }

    public void PlayStartedFired()
    {

    }
}
