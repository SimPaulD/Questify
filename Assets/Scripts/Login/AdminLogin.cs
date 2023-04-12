using Firebase.Auth;
using UnityEngine;

public class AdminLogin : MonoBehaviour
{
    public GameObject adminButton;
    public GameObject adminPanel;
    public string adminEmail;
    private FirebaseAuth auth;

    private void OnEnable()
    {
        if (auth != null)
        {
            auth.StateChanged += AuthStateChanged;
            AuthStateChanged(this, null);
        }
    }

    private void OnDisable()
    {
        if (auth != null)
        {
            auth.StateChanged -= AuthStateChanged;
        }
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        Debug.Log("Auth state changed");
        if (auth.CurrentUser != null && auth.CurrentUser.Email == adminEmail)
        {
            adminButton.SetActive(true);
            Debug.Log("Admin button activated");
        }
        else
        {
            adminButton.SetActive(false);
            adminPanel.SetActive(false);
            Debug.Log("Admin button deactivated");
        }
    }

    public void ClosePanel()
    {
        adminPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        adminPanel.SetActive(true);
    }
}