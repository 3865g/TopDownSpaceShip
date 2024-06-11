using Assets.Scripts.UI.Menu;
using Scripts.Hero.Ability;
using Scripts.Infrastructure.States;
using Scripts.UI.Services.Windows;
using Scripts.UI.Windows.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Elements
{
    public class ChangeConfiguration : MonoBehaviour
    {
        public Button Button;
        private GameObject _abilityManager;
        public SkillType skillType;

        

        private void OnEnable()
        {
            Button.onClick.AddListener(ChangeShipConfiguration);

        }

        public void ChangeShipConfiguration()
        {
            //Need Refactoring
            _abilityManager = GameObject.FindWithTag("AbilityManager");
            _abilityManager.GetComponent<AbilityManager>().ChangeConfiguration(skillType);
            Debug.Log(skillType);
        }
    }
}