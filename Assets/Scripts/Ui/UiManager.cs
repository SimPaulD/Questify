using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public GameObject loginTab;
    public GameObject registerTab;
    public GameObject userProfileTab;
    //public GameObject scoreboardTab;

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

    public void disableAllTabs()
    {
        loginTab.SetActive(false);
        registerTab.SetActive(false);
        userProfileTab.SetActive(false);
        //scoreboardTab.SetActive(false);
    }

    public void LoginTab() 
    {
        disableAllTabs();
        loginTab.SetActive(true);
    }
    public void RegisterTab() 
    {
        disableAllTabs();
        registerTab.SetActive(true);
    }

    public void UserProfileTab()
    {
        disableAllTabs();
        userProfileTab.SetActive(true);
    }

    /*public void ScoreboardTab()
    {
        disableAllTabs();
        scoreboardTab.SetActive(true);
    }*/
}