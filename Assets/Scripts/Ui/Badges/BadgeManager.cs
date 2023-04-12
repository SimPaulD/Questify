using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Auth;

public class BadgeManager : MonoBehaviour
{
    public BadgeData[] badges;
    private bool updatingUI = false;
    private DatabaseReference databaseRef;
    private FirebaseAuth auth;
    private string userID;

    void Start()
    {
        // Get reference to Firebase database
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        // Get ID of current user
        userID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        // Set up listener for changes to user's unlocked badges in Firebase database
        databaseRef.Child("users").Child(userID).Child("unlockedBadges").ValueChanged += HandleValueChanged;

        // Check current unlocked badges and update UI
        UpdateBadgeUI();
    }

    void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        // Check if UI is currently being updated
        if (updatingUI)
        {
            return;
        }

        // Update UI when user's unlocked badges change in Firebase database
        UpdateBadgeUI(args);
    }

    void UpdateBadgeUI(ValueChangedEventArgs args = null)
    {
        updatingUI = true;
        // Loop through all badge objects
        for (int i = 0; i < badges.Length; i++)
        {
            // Check if badge is unlocked for current user
            bool isUnlocked = false;
            if (args != null && args.Snapshot != null && args.Snapshot.Child(badges[i].image.name).Exists)
            {
                isUnlocked = (bool)args.Snapshot.Child(badges[i].image.name).Value;
            }

            // Update badge UI based on unlocked status
            if (isUnlocked)
            {
                badges[i].image.color = Color.white; // Unlocked, set to normal color
                badges[i].isUnlocked = true;
            }
            else
            {
                badges[i].image.color = Color.grey; // Locked, set to grey
                badges[i].isUnlocked = false;
            }
        }
        updatingUI = false;
    }

    public void CheckScoreAndUnlockBadges(int score)
    {
        // Loop through all badge objects
        for (int i = 0; i < badges.Length; i++)
        {
            // Check if badge is already unlocked
            if (badges[i].isUnlocked)
            {
                continue;
            }

            // Check if score is high enough to unlock badge
            if (score >= badges[i].scoreToUnlock)
            {
                // Unlock badge
                badges[i].isUnlocked = true;
                badges[i].image.color = Color.white;

                // Save unlocked badge to Firebase database
                databaseRef.Child("users").Child(userID).Child("unlockedBadges").Child(badges[i].image.name).SetValueAsync(true);
            }
        }
    }

    public void LoadBadges()
    {
        // Get unlocked badge data from Firebase database
        databaseRef.Child("users").Child(userID).Child("unlockedBadges").GetValueAsync().ContinueWith(task =>
        {
        if (task.IsFaulted)
        {
            Debug.LogError(task.Exception.Message);
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            // Loop through all badge objects
            for (int i = 0; i < badges.Length; i++)
            {
                // Check if badge is unlocked for current user
                bool isUnlocked = false;
                if (snapshot.Child(badges[i].image.name).Exists)
                {
                    isUnlocked = (bool)snapshot.Child(badges[i].image.name).Value;
                }

                // Update badge UI based on unlocked status
                if (isUnlocked)
                {
                    badges[i].image.color = Color.white; // Unlocked, set to normal color
                    badges[i].isUnlocked = true;
                    }
                    else
                    {
                        badges[i].image.color = Color.grey; // Locked, set to grey
                        badges[i].isUnlocked = false;
                    }
                }
            }
        });
    }
}