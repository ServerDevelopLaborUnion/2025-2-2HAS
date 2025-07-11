using UnityEngine;

namespace KHG.UIs
{
    public class Panel : MonoBehaviour
    {
        public void SetActive(bool value)
        {
            if (value)
                OpenProgress();
            else
                CloseProgress();
        }

        private void OpenProgress()
        {
            print($"OPEN:{gameObject.name}");
        }
        private void CloseProgress()
        {
            gameObject.SetActive(false);
            print($"CLOSE:{gameObject.name}");
        }
    }

}