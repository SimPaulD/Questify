using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject loginTab;
    public GameObject registerTab;
    public GameObject userProfileTab;
    public GameObject accountTab;
    public GameObject badgesTab;
    public GameObject dashboardTab;
    public GameObject leaderboardTab;
    public GameObject questTab;
    public GameObject settingsTab;

    public void Start()
    {
        LoginTab();
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists!");
            Destroy(this);
        }
    }

    public void disableAuthTabs()
    {
        loginTab.SetActive(false);
        registerTab.SetActive(false);
        userProfileTab.SetActive(false);
    }

    public void disableAllTabs()
    {
        dashboardTab.SetActive(false);
        questTab.SetActive(false);
        badgesTab.SetActive(false);
        leaderboardTab.SetActive(false);
        accountTab.SetActive(false);
        settingsTab.SetActive(false);
    }

    public void LoginTab() 
    {
        disableAuthTabs();
        loginTab.SetActive(true);
    }
    public void RegisterTab() 
    {
        disableAuthTabs();
        registerTab.SetActive(true);
    }

    public void UserProfileTab()
    {
        disableAuthTabs();
        userProfileTab.SetActive(true);
    }

    public void DashboardTab()
    {
        disableAllTabs();
        dashboardTab.SetActive(true);
    }

    public void QuestTab()
    {
        disableAllTabs();
        questTab.SetActive(true);
    }

    public void BadgesTab()
    {
        disableAllTabs();
        badgesTab.SetActive(true);
    }

    public void LeaderboardTab()
    {
        disableAllTabs();
        leaderboardTab.SetActive(true);
    }

    public void AccountTab()
    {
        disableAllTabs();
        accountTab.SetActive(true);
    }

    public void SettingsTab()
    {
        disableAllTabs();
        settingsTab.SetActive(true);
    }
}