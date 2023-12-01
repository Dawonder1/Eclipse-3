using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    int dailyReward = 200;
    void play(){
        SceneManager.GetSceneByName("Gym");
    }

    void exit(){
        Application.Quit();
    }

    void ClaimDailyReward(){
        //check for last play date
        PlayerPrefs.SetString("lastPlayDate",DateTime.Today.ToString());
        int coins = PlayerPrefs.GetInt("coinsOwned",0);
        PlayerPrefs.SetInt("coinsOwned",coins + dailyReward);
    }
}
