using Unity.Netcode;
using UnityEngine;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

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
 * 0.1.0    27-Oct-2025 Create based on 
 **/

[HelpURL("https://github.com/BriarSMC/GreatMindsGame/wiki/Player.cs-HelpURL-Page")]
public class Player : NetworkBehaviour
{
    /*
     * The Player object exists solely to serve as the spawn object for NetworkManager.
     * We do almost nothing other than store our object instance in the GameManager should
     * we ever decide we need to do something with the Player object.
     */

    private GameManager gameManager;

    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null) throw new Exception("Could not find GameManager object.");
    }


    public override void OnNetworkSpawn()
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> Player has spawned");
        gameManager.Player = this;
    }
}
