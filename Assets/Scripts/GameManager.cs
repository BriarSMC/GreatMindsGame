using Unity.Netcode;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using TMPro;
using System.Linq;
using System.Net;

public partial class GameManager : NetworkBehaviour
{
    public Player Player;
    // public ulong ClientId;
    // public string ConnectionType;
    // public string OurIPAddress;
    // public string OurHostNumber;

    // [SerializeReference] public Dictionary<ulong, string> playerData = new Dictionary<ulong, string>();

    // private NetworkManager networkManager;
    // [SerializeReference] TMP_Text header;
    // [SerializeReference] TMP_Text outputArea;
    // [SerializeReference] TMP_Text hostIPText;
    // [SerializeReference] TMP_InputField hostIPInput;

    // int msgCount = 0;

    // void Awake()
    // {
    //     networkManager = FindFirstObjectByType<NetworkManager>();
    //     if (networkManager == null) throw new Exception("Could not get NetworkManager");
    // }

    // void Start()
    // {
    //     NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
    //     SetUpButtons();
    //     outputArea.text = "";

    //     GetOurIPAddress();

    // }

    // private void GetOurIPAddress()
    // {
    //     string hostName = Dns.GetHostName();
    //     Debug.Log($"Our Host Name: {hostName}");
    //     IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
    //     OurIPAddress = hostEntry.AddressList[0].ToString();
    //     Debug.Log($"Our IP Address: {OurIPAddress}");
    //     OurHostNumber = OurIPAddress.Split('.').Last();
    // }

    // public void OnClientConnectedCallback(ulong clientId)
    // {
    //     if (clientId != NetworkManager.Singleton.LocalClientId) return;

    //     ClientId = clientId;
    //     Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> clientId: {clientId}/ClientId: {ClientId}");

    //     AddPlayerRpc(ClientId, "NameNotSet");
    //     SetHeaderText();
    // }

    // private void AppendToOutput(string s)
    // {
    //     outputArea.text += $"{++msgCount}: {s}\n";
    // }

    // private void SetHeaderText(string s)
    // {
    //     header.text = $"ClientId: {ClientId}  Type: {ConnectionType}";
    //     hostIPText.text = s;
    // }

    // private void SetHeaderText()
    // {
    //     header.text = $"ClientId: {ClientId}  Type: {ConnectionType}";
    // }

    // [Rpc(SendTo.Server)]
    // public void AddPlayerRpc(ulong clientId, string name)
    // {
    //     if (!IsServer) return;

    //     Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> Adding ID: {clientId}  Name: {name}");
    //     playerData.Add(clientId, name);
    //     Debug.Log(playerData.Count);
    //     AppendToOutput($"{clientId} connected.");
    // }

    // [Rpc(SendTo.Server)]
    // public void SetPlayerNameRpc(ulong clientId, string name)
    // {
    //     if (!IsServer) return;

    //     playerData[clientId] = name;

    //     AppendToOutput($"{clientId} name changed to {name}");

    //     Debug.Log($"{this.name}:{MethodBase.GetCurrentMethod().Name}> New Name set from: {clientId}  Name: {name}");
    //     Debug.Log($"playerData contains:");
    //     foreach (var kvp in playerData)
    //     {
    //         Debug.Log($"ID: {kvp.Key}  Name: {kvp.Value}");
    //     }
    // }

    // [Rpc(SendTo.Server)]
    // public void RequestDictionaryRpc()
    // {
    //     string output = "";
    //     foreach (var kvp in playerData)
    //     {
    //         output += $"ID:{kvp.Key} Name:{kvp.Value}\n";
    //     }
    //     SendDictionaryRpc(output);
    // }

    // [Rpc(SendTo.ClientsAndHost)]
    // public void SendDictionaryRpc(string s)
    // {
    //     AppendToOutput(s);
    // }
}