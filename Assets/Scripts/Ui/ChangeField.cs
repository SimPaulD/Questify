using UnityEngine.EventSystems;   
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ChangeField : MonoBehaviour   
{
    EventSystem _eventSystem;    
    public Selectable firstInput;


    void Start()   
    {
        _eventSystem = EventSystem.current;    
        firstInput.Select();    
    }

    void Update()    
    {
        NextField();    
        PreviousField();   
    }


    public void NextField()    
    {
        if (Input.GetKeyDown(KeyCode.Tab))   
        {
            Selectable next = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();    // Find the next selectable object below the currently selected object
            if (next != null)    // If a selectable object was found
            {
                next.Select();    // Select the next selectable object
            }
        }
    }


    public void PreviousField()    
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Tab))   
        {
            Selectable previous = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();    // Find the previous selectable object above the currently selected object
            if (previous != null)    // If a selectable object was found
            {
                previous.Select();    // Select the previous selectable object
            }
        }
    }
}