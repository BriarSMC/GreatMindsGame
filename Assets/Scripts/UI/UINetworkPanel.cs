using UnityEngine;
using CoghillClan.PanelManager;
public class UINetworkPanel : MonoBehaviour
{
    PanelManager panelManager;

    void Start()
    {
        panelManager = GameObject.Find("PanelManager").GetComponent<PanelManager>();
    }



    void Update()
    {

    }
}
