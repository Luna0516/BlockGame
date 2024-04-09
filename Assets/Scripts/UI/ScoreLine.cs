using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreLine : MonoBehaviour
{
    public GameObject newImage;

    public TextMeshProUGUI scoreText;

    public void NewImage(bool _new)
    {
        newImage.SetActive(_new);
    }

    public void ScoreText(int score)
    {
        if(score == 0)
        {
            scoreText.text = "-";
        }
        else
        {
            scoreText.text = $"{score:N0}";
        }
    }
}
