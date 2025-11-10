using CoghillClan.PanelManager;
using UnityEngine;
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
 * 0.1.0    27-Oct-2025 From Scratch
 **/

public class UIManager : MonoBehaviour
{
    /*
     * Really serves no purpose other than to make sure the PanelManager script 
     * is attached to a child object.
     */

    [SerializeReference] PanelManager panelManager;

    void Start()
    {
        panelManager = GetComponentInChildren<PanelManager>();

        if (panelManager == null) throw new ApplicationException("Error in UIManger: Could not find PanelManager.");

    }


}
