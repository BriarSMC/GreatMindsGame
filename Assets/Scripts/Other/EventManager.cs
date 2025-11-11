using System;
using System.Collections.Generic;
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
//FIXME Refactor to class GameEvents{}
class EventManager : MonoBehaviour
{
    /*
     * EventManager is a persistent object used to define UnityEvent objects
     * used for controlling the game.
     *
     * GreatMinds is an event driven game. Events control all game logic.
     * Other code in the game either subscribe to the events below or invoke 
     * them as needed.
     */

    // Our instance variable
    public static EventManager Instance;

    // Events
    public static UnityEvent SplashScreenFinished = new UnityEvent();
    public static UnityEvent<string> PlayerNameSet = new UnityEvent<string>();
    public static UnityEvent PlayStarted = new UnityEvent();
    public static UnityEvent HostBtnClicked = new UnityEvent();
    public static UnityEvent<string> ConnectBtnClicked = new UnityEvent<string>();
    public static UnityEvent<Dictionary<ulong, string>> NewPlayerListAvailable = new UnityEvent<Dictionary<ulong, string>>();
    public static UnityEvent UpdateHostsPlayerList = new UnityEvent();
    public static UnityEvent QuitBtnClicked = new UnityEvent();


    private void Awake()
    {
        /*
         * Set up as a persistent game object
         */

        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}