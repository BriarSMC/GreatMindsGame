using UnityEngine;
using CoghillClan.PanelManager;
using TMPro;
using UnityEngine.UI;

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
 * 0.1.0    01-Nov-2025 From scratch
 **/

public class HostPanel : Panel
{
    GameManager gameManager;
    TextMeshProUGUI playerNamePrefab;

    TextMeshProUGUI hostNumberText;
    Button startGameBtn;
    Button quitBtn;
    RectTransform playerListPanel;



    public override void OnPanelLoaded()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject obj = Resources.Load<GameObject>("Prefabs/ConnectedPlayerText");
        playerNamePrefab = Instantiate(obj).GetComponent<TextMeshProUGUI>();
        hostNumberText = transform.Find("HostNumberText").GetComponent<TextMeshProUGUI>();
        startGameBtn = transform.Find("StartGameBtn").GetComponent<Button>();
        startGameBtn.onClick.AddListener(OnStartGameBtnClicked);
        quitBtn = transform.Find("QuitBtn").GetComponent<Button>();
        quitBtn.onClick.AddListener(gameManager.QuitGame);
        playerListPanel = transform.Find("PlayerListPanel").GetComponent<RectTransform>();
    }

    public override void OnPanelEnabled()
    {
        hostNumberText.text = $"Game #{gameManager.OurNodeNumber}";
    }

    private void OnStartGameBtnClicked()
    {
        playerNamePrefab.text = $"Player: {gameManager.Player.PlayerName}";
        playerNamePrefab.transform.SetParent(playerListPanel, false);
        EventManager.PlayStarted.Invoke();
    }
}
