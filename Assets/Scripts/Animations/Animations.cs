using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    FirebaseManagerAuth auth;
    Animation anim;
    void Start()
    {
        anim = GetComponent<Animation>();
        auth = GameObject.Find("FirebaseManagerAuth").GetComponent<FirebaseManagerAuth>();
        
        if(auth.isLoggedIn)
        {
            UserProfileOnLogin();
        }
    }

    void Update()
    {
        
    }

    private void UserProfileOnLogin()
    {
        anim.Play("Initialize");
    }
}
