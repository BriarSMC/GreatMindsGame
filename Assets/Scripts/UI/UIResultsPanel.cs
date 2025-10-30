using UnityEngine;
using CoghillClan.PanelManager;
public class UIResultsPanel : MonoBehaviour
{
    PanelManager panelManager;

    void Start()
    {
        panelManager = GameObject.Find("PanelManager").GetComponent<PanelManager>();
    }

}
