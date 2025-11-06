using Unity.Netcode;
using UnityEngine;
using UnityEditor;

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
 * 0.1.0    03-Nov-2025 From scratch
 **/

public partial class GameManager : NetworkBehaviour
{
    public void QuitGame()
    {
#if UNITY_EDITOR
        // This code will only run in the Unity Editor
        EditorApplication.isPlaying = false;
#else
        // This code will run in a built application (though it won't be called if ExitGameInEditor is only for editor)
        Application.Quit(); 
#endif
    }

}
