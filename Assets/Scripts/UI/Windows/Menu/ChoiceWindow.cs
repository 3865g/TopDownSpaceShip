using Scripts.Hero.Ability;
using Scripts.Infrastructure.States;
using TMPro;
using UnityEngine.UI;

namespace Scripts.UI.Windows.Menu
{
    public class ChoiceWindow : WindowBase
    {

        public Button Button1;
        public Button Button2;

        public TextMeshProUGUI Button1Text;
        public TextMeshProUGUI Button2Text;
        public TextMeshProUGUI MainTextHeading;
        public TextMeshProUGUI MainTextBody;


        public void Construct()
        {
        }


        private void Awake()
        {
            Button1.onClick.AddListener(Choice1);

            Button2.onClick.AddListener(Choice2);
        }

        public void Choice1()
        {

        }

        public void Choice2()
        {

        }
    }
}
