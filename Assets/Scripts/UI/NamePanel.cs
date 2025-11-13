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
    /*
     * This panel allows the player to set their name
     */

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
        quitBtn.onClick.AddListener(() => GameEvents.QuitBtnClicked.Invoke());
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
        /*
         * String can't be blank or null. 
         * String can contain only Alphanumeric and the hyphen.
         */

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

        GameEvents.PlayerNameSet.Invoke(name);
    }

    private void DisplayError(string msg)
    {
        /*
         * To display an error message we have to "enable" the text field by
         * turning its alpha channel all the way on. Then set the message text.
         */

        errorMessageGroup.alpha = 1f;
        errorMessageText.text = msg;
    }
}
