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

        //roomOwnerTmp.SetText("방 주인장!");
        //추후 Player가 추가되면 UI에 설정하는거 만들것
    }

    private void CheckPlayerCount(int cur,int max)
    {
        connectButton.interactable = cur < max;
    }
}
