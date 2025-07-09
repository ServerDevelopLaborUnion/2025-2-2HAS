using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomNameTmp;
    [SerializeField] private TextMeshProUGUI roomCountTmp;
    [SerializeField] private TextMeshProUGUI roomOwnerTmp;

    [SerializeField] private Button connectButton;
    public string Name
    {
        get => gameObject.name;
        set => gameObject.name = value;
    }

    public void SetRoomInfo(RoomInfoPacket info)
    {
        CheckPlayerCount(info.currentCount,info.maxCount);

        roomNameTmp.SetText(info.roomName);
        
        roomCountTmp.SetText($"{info.currentCount}/{info.maxCount}");

        roomOwnerTmp.SetText(info.hostName);
    }

    private void CheckPlayerCount(int cur,int max)
    {
        connectButton.interactable = cur < max;
    }
}
