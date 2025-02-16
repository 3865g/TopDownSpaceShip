using Scripts.Data;
using Scripts.Enemy;
using Scripts.Infrastructure.States;
using Scripts.Services.PersistentProgress;
using Scripts.UI.Services.Windows;
using UnityEngine;

namespace Scripts.Hero
{
    [RequireComponent(typeof(HeroDeath))]
    public class HeroDeath : MonoBehaviour
    {
        public HeroHealth heroHealth;
        public ShipMove shipMove;

        public GameObject DeathFx;

        public string Button1Text = "menu_mainMenu_button";
        public string ChoiseHeadding = "menu_death_choise_heading";
        public string ChoiseBody = "menu_death_choise_body";

        private string _mainMenu = "MainMenu";


        private HeroAttack _heroAttack;
        private bool _isDead =false;
        private Rigidbody _rigidbody;
        private IGameStateMachine _gameStateMachine;
        private IWindowService _windowService;
        private IPersistentProgressService _persistentProgressService;

        public void Construct(IGameStateMachine gameStateMachine, IWindowService windowService, IPersistentProgressService persistentProgressService)
        {
            _gameStateMachine = gameStateMachine;
            _windowService = windowService;
            _persistentProgressService = persistentProgressService;
        }

        private void Awake()
        {
            _heroAttack = GetComponent<HeroAttack>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            heroHealth.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            heroHealth.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if(!_isDead && heroHealth.CurrentHP <= 0)
            {
                Die();

            }
            //Debug.Log(heroHealth.CurrentHP);
        }

        private void Die()
        {
            _isDead = true;
            shipMove.enabled = false;
            _heroAttack.enabled = false;
            //Animator.Play(Die);

            //Destroy(gameObject);
            gameObject.SetActive(false);

            Instantiate(DeathFx, transform.position, Quaternion.identity);

            Invoke("GameOverWindow", 1f);

            

            //Debug.Log(heroHealth.CurrentHP);

        }

        public void GameOverWindow()
        {
            CreateChoiseWindow();
            
        }

        public void CreateChoiseWindow()
        {
            _windowService.Open(WindowId.Confim);
            _windowService.ConfimWindow.Construct(Button1Text, ChoiseHeadding, ChoiseBody);
            _windowService.ConfimWindow.Choice1 += GameOver;
        }


        public void GameOver()
        {
            _persistentProgressService.Progress = null;
            _gameStateMachine.Enter<LoadLevelState, string>(_mainMenu);
        }

        public PlayerProgress NewProgress()
        {
            var progress = new PlayerProgress(initialLevel: _mainMenu);

            //Need Refactoring, load vcalues from SO

            progress.HeroStats.Damage = 10;
            progress.HeroStats.DamageRadius = 20;
            progress.HeroState.MaxHP = 50;
            progress.HeroState.ResetHP();



            return progress;
        }

    }
}