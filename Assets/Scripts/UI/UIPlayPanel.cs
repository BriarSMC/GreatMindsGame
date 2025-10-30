using UnityEngine;
using CoghillClan.PanelManager;
public class UIPlayPanel : MonoBehaviour
{
    PanelManager panelManager;

    void Start()
    {
        panelManager = GameObject.Find("PanelManager").GetComponent<PanelManager>();
    }

}
