using TMPro;
using UnityEngine;

public class PlayerNickname : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputText;

    private string nickname;
    public void ApplyName()
    {
        nickname = inputText.text;
    }
}
