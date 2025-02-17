using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class ChangeLocalization : MonoBehaviour /*ISavedSettings*/
{
    private bool active = false;



 

    public void ChangeLocate(int localeID)
    {
        if (active)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }


    IEnumerator SetLocale(int localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
        active = false;
    }
}
