using System.Collections;
using UnityEngine;
using Firebase;
using TMPro;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using System.Linq;

public class FirebaseManagerAuth : MonoBehaviour
{
    //Firebase 
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference Database;

    //Register 
    [Header("Register")]
    public TMP_InputField usernameRegister;
    public TMP_InputField emailRegister;
    public TMP_InputField passwordRegister;
    public TMP_InputField passwordRegisterConfirm;
    public TMP_Text warningRegister;
    public Button showPasswordReg;
    public TMP_Text showPasswordRegText;

    //Login 
    [Header("Login")]
    public TMP_InputField emailLogin;
    public TMP_InputField passwordLogin;
    public TMP_Text warningLogin;
    public TMP_Text confirmLogin;
    public Button showPasswordLog;
    public TMP_Text showPasswordLogText;

    //User data
    [Header("User")]
    public TMP_InputField userNameChange;
    public TMP_Text userName;
    public TMP_Text userPoints;
    public TMP_Text userScore;

    public GameObject scoreObj;
    public Transform leaderboardContent;


    //Bools
    [Header("Bools")]
    public bool buttonPressed = false;


    void Start()
    {
        //Change password input from **** to •••
        passwordRegister.inputType = TMP_InputField.InputType.Password;
        passwordRegister.asteriskChar = '•';
        passwordRegisterConfirm.inputType = TMP_InputField.InputType.Password;
        passwordRegisterConfirm.asteriskChar = '•';
        passwordLogin.inputType = TMP_InputField.InputType.Password;
        passwordLogin.asteriskChar = '•';
    }

    void Update()
    {
        ButtonDownPasswordLogin();
        ButtonUpPasswordLogin();

    }

    void Awake()
    {
        //Check  dependencies for Firebase that are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance
        auth = FirebaseAuth.DefaultInstance;
        Database = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLogin.text, passwordLogin.text));
    }
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegister.text, passwordRegister.text, usernameRegister.text));
    }
    public void SignOutButton()
    {
        auth.SignOut();
        UiManager.instance.LoginTab();
        ClearRegisterFeilds();
        ClearLoginFeilds();
    }

    public void ClearLoginFeilds()
    {
        emailLogin.text = "";
        passwordLogin.text = "";
    }
    public void ClearRegisterFeilds()
    {
        usernameRegister.text = "";
        emailRegister.text = "";
        passwordRegister.text = "";
        passwordRegisterConfirm.text = "";
    }

    public void ButtonDownPasswordLogin()
    {
        if(buttonPressed)
        {
            passwordLogin.contentType = TMP_InputField.ContentType.Standard;
            passwordLogin.ForceLabelUpdate();
            showPasswordLogText.text = "Release to hide the password";
        }
    }

    public void ButtonUpPasswordLogin()
    {
        if (!buttonPressed)
        {
            passwordLogin.contentType = TMP_InputField.ContentType.Password;
            passwordLogin.ForceLabelUpdate();
            showPasswordLogText.text = "Press to the show Password";
        }
    }

    public void LeaderboardButton()
    {
        StartCoroutine(LoadLeaderboardData());
    }

    public void ButtonIsPressed()
    {
            buttonPressed= true;
    }

    public void ButtonIsNotPressed()
    {
            buttonPressed= false;
    }

    public void SaveUserData()
    {
        StartCoroutine(UpdateUsernameAuth(userNameChange.text));
        StartCoroutine(UpdateUsernameDatabase(userNameChange.text));
        StartCoroutine(UpdateUsernameAuth(userName.text));
        StartCoroutine(UpdateUsernameDatabase(userName.text));
    }

    public void SaveUserStats()
    {
        Debug.Log("Hatz");
       StartCoroutine(UpdateScore(int.Parse(userScore.text)));
       StartCoroutine(UpdatePoints(int.Parse(userPoints.text)));
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLogin.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLogin.text = "";
            confirmLogin.text = "Logged In";
            yield return new WaitForSeconds(1);
            confirmLogin.text = "Loading profile";
            yield return new WaitForSeconds(2);
            userNameChange.text = User.DisplayName;
            userName.text = User.DisplayName;
            confirmLogin.text = "";
            StartCoroutine(LoadUserData());
            UiManager.instance.UserProfileTab();
            ClearRegisterFeilds();
            ClearLoginFeilds();

        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {

        if (_username == "")
        {
            //If the username field is empty show a warning
            warningRegister.text = "Missing Username";
        }
        else if (passwordRegister.text != passwordRegisterConfirm.text)
        {
            //If the password does not empty show a warning
            warningRegister.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register failed, invalid E-mail adress! (example@example.com)";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegister.text = message;
            }
            else
            {
                //User has been created
                //Get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegister.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is  set
                        //Return to login tab and insert e-mail
                        UiManager.instance.LoginTab();
                        warningRegister.text = "";
                        confirmLogin.text = "Your accound was create, Login!";
                        emailLogin.text = _email;
                        ClearRegisterFeilds();
                        ClearLoginFeilds();
                    }
                }
            }
        }
    }

    private IEnumerator UpdateUsernameAuth(string _username)
    {
        //Create a user profile and set the username
        UserProfile profile = new UserProfile { DisplayName = _username };

        //Call the Firebase auth update user profile function passing the profile with the username
        var ProfileTask = User.UpdateUserProfileAsync(profile);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

        if (ProfileTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
        }
        else
        {
            //Username is  updated
        }
    }

    private IEnumerator UpdateUsernameDatabase(string _username)
    {
        //Set the currently logged in user username in the database
        var DBTask = Database.Child("users").Child(User.UserId).Child("username").SetValueAsync(_username);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is updated
        }
    }

    private IEnumerator UpdateScore(int _score)
    {
        //Set the currently logged in user Score
        var DBTask = Database.Child("users").Child(User.UserId).Child("score").SetValueAsync(_score);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Score is updated
        }
    }

    private IEnumerator UpdatePoints(int _points)
    {
        //Set the currently logged in user points
        var DBTask = Database.Child("users").Child(User.UserId).Child("points").SetValueAsync(_points);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Points are now updated  
        }
    }

    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = Database.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            userScore.text = "0";
            userPoints.text = "0";
            
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            userScore.text = snapshot.Child("score").Value.ToString();
            userPoints.text = snapshot.Child("points").Value.ToString();

        }
    }

    private IEnumerator LoadLeaderboardData()
    {
        //Get all the users data ordered by points amount
        var DBTask = Database.Child("users").OrderByChild("score").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing leaderboard elements
            foreach (Transform child in leaderboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int points = int.Parse(childSnapshot.Child("points").Value.ToString());
                int score = int.Parse(childSnapshot.Child("score").Value.ToString());

                //Instantiate new leaderboard elements
                //GameObject scoreObj = Instantiate(scoreObj, leaderboardContent);
                scoreObj.GetComponent<ScoreObj>().NewScoreElement(username, points, score);
            }
        }
    }


}
