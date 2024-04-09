using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLines : MonoBehaviour
{
    public ScoreLine[] lines;

    Score[] scores = new Score[3];

    public struct Score
    {
        public bool isNewScore;
        public int score;
    }

    private void Awake()
    {
        DataManager.Inst.onDataSet += () =>
        {
            for(int i = 0; i < 3; i++)
            {
                scores[i].isNewScore = DataManager.Inst.isNewScores[i];
                scores[i].score = DataManager.Inst.scores[i];
            }

            for (int i = 0; i < 3; i++)
            {
                lines[i].NewImage(scores[i].isNewScore);
                lines[i].ScoreText(scores[i].score);
            }
        };
    }
}
