using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BadgeScriptableObject", menuName = "ScriptableObjects/Badge")] 

public class BadgeScriptableObject : ScriptableObject
{
    public bool unlocked;
    public string badgeName;
    public Image image;
}
