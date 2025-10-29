using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public partial class GameManager : NetworkBehaviour
{

    // Panic Codes
    public enum PanicCode
    {
        NoGameManagerFound,
        NoNetworkManagerFound,
    }


    // Panic Code Messages
    public static readonly Dictionary<int, string> PanicMessageText = new Dictionary<int, string>()
  {
    {(int) PanicCode.NoGameManagerFound, "Could not find the GameManager." },
    {(int) PanicCode.NoNetworkManagerFound, "Could not find the NetworkManager."},
  };
}
