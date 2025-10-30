using UnityEngine;
using CoghillClan.PanelManager;
public class UIMessagePanel : MonoBehaviour
{
    PanelManager panelManager;

    void Start()
    {
        panelManager = GameObject.Find("PanelManager").GetComponent<PanelManager>();
    }

}
