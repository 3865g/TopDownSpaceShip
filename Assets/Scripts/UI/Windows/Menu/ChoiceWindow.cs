using Scripts.Hero.Ability;
using Scripts.Infrastructure.States;
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


        //public void Construct(string button1Text, string button2Text, string headding, string body)
        //{
        //    _button1Text.text = button1Text;
        //    _button2Text.text = button2Text;
        //    _mainTextHeading.text = headding;
        //    _mainTextBody.text = body;
        //}


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
    }
}
