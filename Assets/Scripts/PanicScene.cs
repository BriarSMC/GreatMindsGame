using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

public class PanicScene : MonoBehaviour
{
    GameManager gameManager;
    Button quitBtn;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        quitBtn.GetComponent<Button>().onClick.AddListener(PanicQuit);
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
}
