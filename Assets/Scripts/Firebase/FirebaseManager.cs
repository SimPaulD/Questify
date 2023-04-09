using System.Collections;
using UnityEngine;
using Firebase;
using TMPro;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.SceneManagement;


public class FirebaseManager : MonoBehaviour
{
    public FirebaseManagerAuth managerLog;
    //User data
    [Header("User Data")]
    public TMP_InputField user;
    public TMP_InputField points;
    public GameObject score;
    public Transform scoreBoardContent;



    public void SignOutButton()
    {
        managerLog.auth.SignOut();
        SceneManager.LoadScene("UserProfile");
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }

    public void ClearLoginFeilds()
    {
        managerLog.emailLogin.text = "";
        managerLog.passwordLogin.text = "";
    }
    public void ClearRegisterFeilds()
    {
        managerLog.usernameRegister.text = "";
        managerLog.emailRegister.text = "";
        managerLog.passwordRegister.text = "";
        managerLog.passwordRegisterConfirm.text = "";
    }

    
}
