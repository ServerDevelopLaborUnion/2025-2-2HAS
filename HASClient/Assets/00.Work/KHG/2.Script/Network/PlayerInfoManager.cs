using AKH.Network;
using DewmoLib.Utiles;
using System.Diagnostics    .Tracing;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO playerInfoChannel;

    private void Awake()
    {
        playerInfoChannel.AddListener<PlayerNameEvent>(SetPlayerName);
    }

    private void SetPlayerName(PlayerNameEvent evt)
    {
        C_SetName c_SetName = new C_SetName();
        c_SetName.name = evt.Name;
        NetworkManager.Instance.SendPacket(c_SetName);
    }
}
