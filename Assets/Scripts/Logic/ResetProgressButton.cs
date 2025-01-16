using Scripts.Services;
using UnityEngine;
using UnityEngine.UI;
using Scripts.Services.PersistentProgress;
using Scripts.Data;

public class ResetProgressButton : MonoBehaviour
{
    public Button Button;
    public string InitialLevel = "MainMenu";



    private IPersistentProgressService _persistentProgressService;


    private void Awake()
    {

        _persistentProgressService = AllServices.Container.Single<IPersistentProgressService>();

        Button.onClick.AddListener(ResetProgress);
    }

    public void ResetProgress()
    {
        _persistentProgressService.Progress = NewProgress();
    }

    public PlayerProgress NewProgress()
    {
        var progress = new PlayerProgress(initialLevel: InitialLevel);

        //Need Refactoring, load vcalues from SO

        progress.HeroStats.Damage = 10;
        progress.HeroStats.DamageRadius = 20;
        progress.HeroState.MaxHP = 50;
        progress.HeroState.ResetHP();
        progress.AbilityProgress = new AbilityData();



        return progress;
    }

}
