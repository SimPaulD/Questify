using UnityEngine;

public class UiManager : MonoBehaviour
{
    private LogRegAnimController _controller;
    public static UiManager instance;

    [Header("Tabs")]
    public GameObject loginTab;
    public GameObject registerTab;
    public GameObject userProfileTab;
    public GameObject accountTab;
    public GameObject badgesTab;
    public GameObject dashboardTab;
    public GameObject leaderboardTab;
    public GameObject questTab;
    public GameObject settingsTab;
    public GameObject shopTab;

    [Header("Bools")]
    public bool[] isTabActive;

    public void Start()
    {
        _controller = GameObject.Find("Log/Reg").GetComponent<LogRegAnimController>();
        LoginTab();
        isTabActive[4] = true;
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
        _controller.checkIfTabActiveLog = false;
        _controller.checkIfTabActiveReg = false;
    }

    public void disableAllTabs()
    {
        dashboardTab.SetActive(false);
        questTab.SetActive(false);
        badgesTab.SetActive(false);
        leaderboardTab.SetActive(false);
        accountTab.SetActive(false);
        settingsTab.SetActive(false);
        shopTab.SetActive(false);
        isTabActive[0] = false;
        isTabActive[1] = false;
        isTabActive[2] = false;
        isTabActive[3] = false;
        isTabActive[4] = false;
        isTabActive[5] = false;
        isTabActive[6] = false;
    }

    public void LoginTab()
    {
        disableAuthTabs();
        loginTab.SetActive(true);
       _controller.checkIfTabActiveLog = true;
    }
    public void RegisterTab()
    {
        disableAuthTabs();
        registerTab.SetActive(true);
        _controller.checkIfTabActiveReg = true;
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
        isTabActive[0] = true;
    }

    public void QuestTab()
    {
        disableAllTabs();
        questTab.SetActive(true);
        isTabActive[1] = true;
    }

    public void BadgesTab()
    {
        disableAllTabs();
        badgesTab.SetActive(true);
        isTabActive[2] = true;
    }

    public void LeaderboardTab()
    {
        disableAllTabs();
        leaderboardTab.SetActive(true);
        isTabActive[3] = true;
    }

    public void AccountTab()
    {
        disableAllTabs();
        accountTab.SetActive(true);
        isTabActive[4] = true;
    }

    public void SettingsTab()
    {
        disableAllTabs();
        settingsTab.SetActive(true);
        isTabActive[5] = true;
    }

    public void ShopTab()
    {
        disableAllTabs();
        shopTab.SetActive(true);
        isTabActive[6] = true;
    }
}