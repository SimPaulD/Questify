using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreObj : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text scoreText;
    public TMP_Text pointsText;

    public void NewScoreElement(string _username, int _points, int _score)
    {
        usernameText.text = _username;
        scoreText.text = _score.ToString();
        pointsText.text = _points.ToString();
    }
}
