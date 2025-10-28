using Unity.Netcode;
using UnityEngine;
using System.Reflection;
using System;

/**
 *
 * Copyright © 2025 by Steven M. Coghill
 * This project is licensed under the MIT License.
 * A copy of the MIT License can be found in the 
 * accompanying LICENSE.txt file.
 **/
/** 
 * https://games.coghillclan.net/
 * 
 * https://www.github.com/BriarSMC/
 *
 * Version: 0.1.0
 * Version History
 * ----------------------------------------------------------------------------
 * nn.nn.nn dd-mmm-yyyy Comment 
 * 0.1.0    27-Oct-2025 Create based on 
 **/
[AddComponentMenu("Enable ActiveSceneSynchronization")]
[HelpURL("https://github.com/BriarSMC/GreatMindsGame/wiki/Player.cs-HelpURL-Page")]
public class Player : NetworkBehaviour
{

    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null) throw new Exception("Could not find GameManager object.");
    }

    public override void OnNetworkSpawn()
    {
        gameManager.Player = this;
    }

}
