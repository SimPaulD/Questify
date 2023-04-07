using UnityEngine.EventSystems;   
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using TMPro.EventSystems;  //is not working


public class ChangeField : MonoBehaviour   
{
    EventSystem _eventSystem;    
    public Selectable firstInput;
    public TextMeshProUGUI _submitButton;


    void Start()   
    {
        _eventSystem = EventSystem.current;    // Get a reference to the event system
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
        if (Input.GetKeyDown(KeyCode.LeftShift))   
        {
            Selectable previous = _eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();    // Find the previous selectable object above the currently selected object
            if (previous != null)    // If a selectable object was found
            {
                previous.Select();    // Select the previous selectable object
            }
        }
    }

    public void Return()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
           // _submitButton.OnPointerClick(null);
        }
    }


}