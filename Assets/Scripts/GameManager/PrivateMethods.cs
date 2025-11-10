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
    // Exit the game here. Do any cleanup work needed,
    public void QuitGame()
    {
#if UNITY_EDITOR        
        EditorApplication.isPlaying = false; // This code will only run in the Unity Editor
#else
        Application.Quit(); // This code will run in a built application (though it won't be called if ExitGameInEditor is only for editor)
#endif
    }

}
