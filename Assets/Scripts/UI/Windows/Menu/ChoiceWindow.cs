using System;
using TMPro;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{
    public class ChoiceWindow : WindowBase
    {

        public Button Button1;
        public Button Button2;

        public Action Choice1;
        public Action Choice2;
        public Action DestroyWindow;

        public TextMeshProUGUI Button1Text;
        public TextMeshProUGUI Button2Text;
        public TextMeshProUGUI MainTextHeading;
        public TextMeshProUGUI MainTextBody;




        private void Awake()
        {
            Button1.onClick.AddListener(Button1Click);

            Button2.onClick.AddListener(Button2Click);
        }

        public void Button1Click()
        {
            Choice1?.Invoke();

            CloseWindow();
        }

        public void Button2Click()
        {
            Choice2?.Invoke();

            CloseWindow();
        }

        public void CloseWindow()
        {
            DestroyWindow?.Invoke();

            Destroy(gameObject);

        }

        //Have close window on parent class but not work
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
