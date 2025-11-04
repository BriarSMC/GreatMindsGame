using UnityEngine;
using CoghillClan.PanelManager;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using System.Reflection;
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

public class NamePanel : Panel
{
    GameManager gameManager;
    TMP_InputField nameInput;
    Button acceptBtn;
    RectTransform errorMessagePanel;
    CanvasGroup errorMessageGroup;
    TMP_Text errorMessageText;
    Button quitBtn;

    Regex invalidChars = new Regex(@"^[a-zA-Z0-9\-]+$");
    bool inputFieldTookFocus = true;

    const string k_ErrorNameIsBlank = "Name is missing.";
    const string k_ErrorInvalidCharacters = "Letters, numbers, hyphens only.";

    public override void OnPanelLoaded()
    {
        gameManager = GameManager.Instance;
        nameInput = transform.Find("NameInputField").GetComponent<TMP_InputField>();
        nameInput.ActivateInputField();
        acceptBtn = transform.Find("AcceptBtn").GetComponent<Button>();
        acceptBtn.onClick.AddListener(OnAcceptClicked);
        errorMessagePanel = transform.Find("ErrorMessagePanel").GetComponent<RectTransform>();
        errorMessageGroup = errorMessagePanel.GetComponent<CanvasGroup>();
        errorMessageGroup.alpha = 0f;
        errorMessageText = errorMessagePanel.Find("ErrorMessageText").GetComponent<TextMeshProUGUI>();
        quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(() => EventManager.QuitBtnClicked.Invoke());
    }
    void Update()
    {
        if (nameInput.isFocused)
        {
            if (inputFieldTookFocus)
            {
                inputFieldTookFocus = false;
                errorMessageText.alpha = 0f;
                nameInput.text = "";
            }
        }
        else
        {
            inputFieldTookFocus = true;
        }
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

        EventManager.PlayerNameSet.Invoke(name);
    }

    private void DisplayError(string msg)
    {
        errorMessageGroup.alpha = 1f;
        errorMessageText.text = msg;
    }
}
