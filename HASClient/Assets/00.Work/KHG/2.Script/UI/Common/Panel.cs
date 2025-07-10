using UnityEngine;

namespace KHG.UIs
{
    public class Panel : MonoBehaviour
    {
        private void Start()
        {
            if (gameObject.activeSelf) gameObject.SetActive(false);
        }
        public void SetActive(bool value)
        {
            if (value)
                OpenProgress();
            else
                CloseProgress();
        }

        private void OpenProgress()
        {

        }
        private void CloseProgress()
        {
            gameObject.SetActive(false);
        }
    }

}