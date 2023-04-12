using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string Title;
    public string Description;
    public string Username;

    public QuestData(string title, string description, string username)
    {
        Title = title;
        Description = description;
        Username = username;
    }
}