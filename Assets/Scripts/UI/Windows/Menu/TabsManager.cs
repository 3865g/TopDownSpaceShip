using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TabsManager : MonoBehaviour
{

    public GameObject ActiveTab;
    public Image ActiveButton;
    public GameObject[] Tabs;
    public Image[] TabButtons;
    public Sprite InactiveTabBG;
    public Sprite ActiveTabBG;
    public Vector2 InactiveTabButtonSize;
    public Vector2 ActiveTabButtonSize;


    private void Awake()
    {
        foreach (GameObject gameObject in Tabs)
        {
            gameObject.SetActive(false);
        }

        SetActiveTab();
    }


    private void OnEnable()
    {
        SetActiveTab();
    }

    public void SwitchToTab(int TabID)
    {
        foreach(GameObject gameObject in Tabs)
        {
            gameObject.SetActive(false);
        }

        Tabs[TabID].SetActive(true);

        if (TabButtons.Length > 0)
        {

            foreach (Image image in TabButtons)
            {
                image.sprite = InactiveTabBG;
                image.rectTransform.sizeDelta = InactiveTabButtonSize;
            }

            TabButtons[TabID].sprite = ActiveTabBG;
            TabButtons[TabID].rectTransform.sizeDelta = ActiveTabButtonSize;
        }
    }

    public void SetActiveTab()
    {
        if (ActiveTab)
        {
            ActiveTab.SetActive(true);
        }
        if (ActiveButton)
        {
            ActiveButton.sprite = ActiveTabBG;
            ActiveButton.rectTransform.sizeDelta = ActiveTabButtonSize;
        }

    }

   
}
