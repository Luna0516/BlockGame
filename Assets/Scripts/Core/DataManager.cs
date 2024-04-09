using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public bool[] isNewScores = new bool[3];
    public int[] scores = new int[3];

    [Range(0f, 1f)]
    public float bgmVolume = 1;

    [Range(0f, 1f)]
    public float effectVolume = 1;

    Data data = new Data();

    string path;
    string fullPath;

    public System.Action onDataSet;

    protected override void OnAwake()
    {
        path = $"{Application.dataPath}/Save/";
        fullPath = $"{path}Save.json";

        LoadData();
    }

    public void SaveData()
    {
        data.isNewScore = isNewScores;
        data.score = scores;
        data.bgmVolume = bgmVolume;
        data.effectVolume = effectVolume;

        string json = JsonUtility.ToJson(data);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        File.WriteAllText(fullPath, json);
    }

    public void LoadData()
    {
        bool result = Directory.Exists(path) && File.Exists(fullPath);

        if (result)
        {
            string json = File.ReadAllText(fullPath);

            Data loadedData = JsonUtility.FromJson<Data>(json);
            isNewScores = loadedData.isNewScore;
            scores = loadedData.score;
            bgmVolume = loadedData.bgmVolume;
            effectVolume = loadedData.effectVolume;
        }
        else
        {
            SetDefaultData();
        }
    }

    public void RankUpdate(int score)
    {
        for(int i = 0; i < 3; i++)
        {
            isNewScores[i] = false;
        }

        for (int i = 0; i < 3; i++)
        {
            if (scores[i] < score)
            {
                for (int j = 2; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }

                isNewScores[i] = true;
                scores[i] = score;

                break;
            }
        }
    }

    private void SetDefaultData()
    {
        for (int i = 0; i < 3; i++)
        {
            isNewScores[i] = false;
            scores[i] = 0;
        }

        bgmVolume = 1;
        effectVolume = 1;
    }
}
