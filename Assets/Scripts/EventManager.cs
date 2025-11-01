using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

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
 * 0.1.0    01-Nov-2025 From scratch
 **/

class EventManager : MonoBehaviour
{
    // Our instance variable
    public static EventManager Instance;

    // Events
    public UnityAction PlayStarted;

    private void Awake()
    {
        // If we exist, destroy this instance and return, otherwise set Instance
        // and tell Unity to never unload us.
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}