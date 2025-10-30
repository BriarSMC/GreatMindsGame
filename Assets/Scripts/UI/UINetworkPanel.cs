using UnityEngine;
using CoghillClan.PanelManager;
using UnityEngine.UI;
using System;
using System.Reflection;
public class UINetworkPanel : MonoBehaviour
{
    GameManager gameManager;
    PanelManager panelManager;
    Button hostBtn;
    Button joinBtn;

    const string k_ButtonsPath = "/UIManager/Canvas/PanelManager/NetworkPanel/ButtonGroup/";

    void Start()
    {
        gameManager = GameManager.Instance;
        panelManager = FindFirstObjectByType<PanelManager>();
        hostBtn = GameObject.Find($"{k_ButtonsPath}HostBtn").GetComponent<Button>();
        hostBtn.onClick.AddListener(HostBtnClicked);
        joinBtn = GameObject.Find($"{k_ButtonsPath}JoinBtn").GetComponent<Button>();
        joinBtn.onClick.AddListener(JoinBtnClicked);
    }

    private void HostBtnClicked()
    {
        gameManager.SetWeAreHost();
        DisplayNextPanel();
    }

    private void JoinBtnClicked()
    {
        gameManager.SetWeArePlayer();
        DisplayNextPanel();
    }

    private void DisplayNextPanel()
    {
        Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> Next Panel: {GameManager.PanelNames[GameManager.Panels.playPanel]}");
        panelManager.Push(GameManager.PanelNames[GameManager.Panels.playPanel]);
    }
}
