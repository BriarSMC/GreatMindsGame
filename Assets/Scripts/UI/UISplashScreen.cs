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
// DELETEME Delete the following line before first rc commit
 * nn.nn.nn dd-mmm-yyyy Comment 
 * 0.1.0    29-Oct-2025 From scratch
 **/
public class UISplashScreen : MonoBehaviour
{
    PanelManager panelManager;

    void Start()
    {
        panelManager = GameObject.Find("PanelManager").GetComponent<PanelManager>();
    }

    public void LoadNewSceneTimelineSignal()
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> Next Panel: {GameManager.PanelNames[GameManager.Panels.namePanel]}");
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.namePanel]);
    }
}
