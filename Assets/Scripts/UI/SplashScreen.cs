using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
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
 * 0.1.0    29-Oct-2025 From scratch
 **/
public class SplashScreen : Panel
{
    /*
     * This is the first panel displayed by the game. The SplashScreenPanel
     * has an animation track to wait a period of time. When that time expires
     * the animation track invokes the method below to fire our own signal that
     * the game can display the next panel.
     */

    public void LoadNewSceneTimelineSignal()
    {
        EventManager.SplashScreenFinished.Invoke();
    }
}
