using AKH.Network;
using DewmoLib.Utiles;
using KHG.Events;
using UnityEngine;

namespace KHG.Managers
{
    public class PlayerInfoManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO playerInfoChannel;

        private void Awake()
        {
            playerInfoChannel.AddListener<PlayerNameEvent>(SetPlayerName);
        }

        private void OnDestroy()
        {
            playerInfoChannel.RemoveListener<PlayerNameEvent>(SetPlayerName);
        }

        private void SetPlayerName(PlayerNameEvent evt)
        {
            C_SetName c_SetName = new C_SetName();
            c_SetName.name = evt.Name;
            NetworkManager.Instance.SendPacket(c_SetName);
        }
    }

}