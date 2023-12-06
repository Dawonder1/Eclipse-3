using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    int dailyReward = 200;
    [SerializeField] GameObject rewardPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] TextMeshProUGUI RewardText;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundSlider;
    public void play(){
        SceneManager.LoadScene("Gym");
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
    }

    public void DailyReward(){
        string rewardText = "Congratulations!\nDaily Reward: "+ dailyReward +" coins";
        string noRewardText = "Daily Reward Claimed!\nCome back Tomorrow.";
        rewardPanel.SetActive(true);
        if(PlayerPrefs.GetString("lastPlayDate","None") == DateTime.Today.ToString()){
            RewardText.text = noRewardText;
        }
        else
        {
            RewardText.text = rewardText;
        }
    }
    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume");
    }

    public void CloseSettingsPanel()
    {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("soundVolume", soundSlider.value);
        settingsPanel.SetActive(false);
    }
}