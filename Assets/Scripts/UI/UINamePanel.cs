using UnityEngine;
using CoghillClan.PanelManager;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Reflection;
public class UINamePanel : MonoBehaviour
{
    GameManager gameManager;
    PanelManager panelManager;
    TMP_InputField nameInput;
    Button acceptBtn;
    RectTransform errorMessagePanel;
    CanvasGroup errorMessageGroup;
    TMP_Text errorMessageText;
    Button quitBtn;

    Regex invalidChars = new Regex(@"^[a-zA-Z0-9\-]+$");


    const string k_ErrorNameIsBlank = "Name is missing.";
    const string k_ErrorInvalidCharacters = "Letters, numbers, hyphens only.";

    void Start()
    {
        gameManager = GameManager.Instance;
        panelManager = FindFirstObjectByType<PanelManager>();
        nameInput = transform.Find("NameInputField").GetComponent<TMP_InputField>();
        nameInput.ActivateInputField();
        acceptBtn = transform.Find("AcceptBtn").GetComponent<Button>();
        acceptBtn.onClick.AddListener(OnAcceptClicked);
        errorMessagePanel = transform.Find("ErrorMessagePanel").GetComponent<RectTransform>();
        errorMessageGroup = errorMessagePanel.GetComponent<CanvasGroup>();
        errorMessageGroup.alpha = 0f;
        errorMessageText = errorMessagePanel.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(gameManager.QuitGame);
    }

    private void OnAcceptClicked()
    {
        string name = nameInput.text;
        if (String.IsNullOrEmpty(name))
        {
            DisplayError(k_ErrorNameIsBlank);
            return;
        }

        if (!invalidChars.IsMatch(name))
        {
            DisplayError(k_ErrorInvalidCharacters);
            nameInput.text = String.Empty;
            return;
        }

        errorMessageGroup.alpha = 0f;
        gameManager.Player.PlayerName = name;
        DisplayNextPanel();
    }

    private void DisplayError(string msg)
    {
        errorMessageGroup.alpha = 1f;
        errorMessageText.text = msg;
    }

    private void DisplayNextPanel()
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> Next Panel: {GameManager.PanelNames[GameManager.Panels.networkPanel]}");
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.networkPanel]);
    }
}
