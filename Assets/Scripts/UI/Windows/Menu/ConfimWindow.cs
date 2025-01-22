using System;
using TMPro;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{
    public class ConfimWindow : WindowBase
    {

        public Button Button1;

        public Action Choice1;

        public TextMeshProUGUI Button1Text;
        public TextMeshProUGUI MainTextHeading;
        public TextMeshProUGUI MainTextBody;


        private void Awake()
        {
            Button1.onClick.AddListener(Button1Click);
        }

        public void Button1Click()
        {
            Choice1?.Invoke();

            CloseWindow();
        }

        public void CloseWindow()
        {
            Destroy(gameObject);
        }

        //Have close window on parent class but not work
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
