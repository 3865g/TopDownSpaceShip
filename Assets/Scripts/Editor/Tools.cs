using Scripts.Infrastructure.States;
using UnityEditor;
using UnityEngine;

namespace Scripts.Editor
{
    public class Tools
    {
        [MenuItem("Tools/Clear prefs")]
        public static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
        //[MenuItem("Tools/Level/L1P1")]
        //public static void ChangeStartLevel()
        //{
        //    IState.LoadProgresState.InitialLevel = "L1P1"; 
        //}


    }
}