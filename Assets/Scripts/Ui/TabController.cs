using Firebase.Auth;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabController : MonoBehaviour
{
    [Header("Firebase")]
    private FirebaseManagerAuth auth;
    public FirebaseUser User;

    [Header("Components")]
    private UiManager uiManager;
    public Animator animator;
    public int index;




    private void Start()
    {
        auth = GameObject.Find("FirebaseManagerAuth").GetComponent<FirebaseManagerAuth>();
        uiManager = GameObject.Find("UiManager").GetComponent<UiManager>();
    }
    private void Update()
    {
        if (auth.isLoggedIn == true)
        {
            if (uiManager.isTabActive[index] == false)
            {
                animator.SetTrigger("Normal");
            }
            else if (uiManager.isTabActive[index] == true)
            {
                // Get the current state of the animator
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

                // Check if the Selected animation is not already playing
                if (!stateInfo.IsName("Selected"))
                {
                    // Set the Selected trigger to play the animation once
                    animator.SetTrigger("Selected");
                }
            }
        }
    }
}