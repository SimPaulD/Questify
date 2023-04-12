using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [Header("Buttons")]
    public GameObject newQuestConfirm;
    //public GameObject newQuestConfirmClose;



    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void NewQuestConfirmOpen() {newQuestConfirm.SetActive(true);}
    public void NewQuestConfirmClose() { newQuestConfirm.SetActive(false); }
}
