using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameConstants : MonoBehaviour
{


    #region Sound
    public static void setSound(bool target)
    {

        if (target)
        {
            PlayerPrefs.SetInt("SOUND", 1);
        }
        else
        {
            PlayerPrefs.SetInt("SOUND", 0);
        }
        PlayerPrefs.Save();
    }
    public static bool isSoundOn()
    {
        return PlayerPrefs.GetInt("SOUND", 1) == 1;
    }

    public static bool isMusicOn()
    {
        return PlayerPrefs.GetInt("MUSIC", 1) == 1;
    }
    public static void SetMusic(bool target)
    {
        if (target)
        {
            PlayerPrefs.SetInt("MUSIC", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MUSIC", 0);
        }
        PlayerPrefs.Save();
    }
    #endregion

}
