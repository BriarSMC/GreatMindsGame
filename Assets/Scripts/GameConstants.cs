using System.Collections.Generic;
using UnityEngine;

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
// DELETEME Delete the following line before first rc commit
 * nn.nn.nn dd-mmm-yyyy Comment 
 * 0.1.0    28-Oct-2025 From scratch
 **/

public class GameConstants : MonoBehaviour
{
    // Panic Codes
    public const int k_PanicCodeNoGameManagerFound = 0;

    // Panic Code Messages
    public static readonly Dictionary<int, string> PanicMessageText = new Dictionary<int, string>()
  {
    {k_PanicCodeNoGameManagerFound, "Could not find the GameManager." },
  };
}
