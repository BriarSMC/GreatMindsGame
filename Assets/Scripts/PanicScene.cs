using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using CoghillClan.PanelManager;

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
 * Version: 0.1.0
 * Version History
 * ----------------------------------------------------------------------------
 * nn.nn.nn dd-mmm-yyyy Comment 
 * 0.1.0    28-Oct-2025 From scratch
 **/

public class PanicScene : Panel
{
    /*
     * At Start get the GameManager object so we can get the PanicCode from it, and
     * connect the Quit our quit method.
     * Display the panic message on the screen.
     * Wait for the Quit Click.
     */

    GameManager gameManager;
    TMP_Text tmpText;
    Button quitBtn;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null) throw new NullReferenceException("PanicScene:Start() -> Cannot find GameManager");
        // gameManager.panelManager.LoadPanels();
        gameManager.panelManager.ManagerEnable(true);

        tmpText = GameObject.Find("MessageText").GetComponent<TMP_Text>();
        quitBtn = GameObject.Find("QuitBtn").GetComponent<Button>();
        quitBtn.GetComponentInChildren<Button>().onClick.AddListener(PanicQuit);

        DisplayPanicMessage();
    }

    private void PanicQuit()
    {
        /*
         * We are keeping this separate from the normal GameManager Quit
         * because we might want to do extra things like send message back
         * to us with crash info.
         */

        gameManager.QuitGame();
    }

    private void DisplayPanicMessage()
    {
        tmpText.text = GameManager.PanicMessageText[gameManager.GetPanicCodeInt()];
    }
}
