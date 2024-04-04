using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Inst.Player.onChangeScore += ScoreChange;

        ScoreChange(GameManager.Inst.Player.Score);
    }

    private void ScoreChange(int score)
    {
        text.text = $"{score:N0}";
    }
}
