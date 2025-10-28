using Unity.Netcode;
using UnityEngine;
using System.Reflection;
using System;

[AddComponentMenu("Enable ActiveSceneSynchronization")]
[HelpURL("http://localhost/mediawiki/index.php?title=Player.cs")]
public class Player : NetworkBehaviour
{

    private GameManager gameManager;
    void Awake()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null) throw new Exception("Could not find GameManager object.");
    }

    void Start()
    {

    }


    void Update()
    {

    }

    public override void OnNetworkSpawn()
    {
        gameManager.Player = this;
    }

}
