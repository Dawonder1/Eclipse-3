using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartMenu : MonoBehaviour
{
    int dailyReward = 200;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] TextMeshProUGUI RewardText;
    public void play(){
        SceneManager.GetSceneByName("Gym");
    }

    public void exit(){
        Application.Quit();
    }

    public void ClaimDailyReward(){
        //check for last play date
        PlayerPrefs.SetString("lastPlayDate", DateTime.Today.ToString());
        int coins = PlayerPrefs.GetInt("coinsOwned",0);
        PlayerPrefs.SetInt("coinsOwned",coins + dailyReward);
        rewardPanel.SetActive(false);
        //
    }

    public void DailyReward(){
        string rewardText = "Congratulations!\nDaily Reward: "+ dailyReward +" coins";
        string noRewardText = "Daily Reward Claimed!\nCome back Tomorrow.";
        rewardPanel.SetActive(true);
        if(PlayerPrefs.GetString("lastPlayDate","None") == DateTime.Today.ToString()){
            RewardText.text = rewardText;
        }
        else
        {
            RewardText.text = noRewardText;
        }
    }
}