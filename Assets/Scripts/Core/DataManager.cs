using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Range(0f, 1f)]
    public float bgmVolume = 1;

    [Range(0f, 1f)]
    public float effectVolume = 1;

    public int[] scores = new int[3];

    Data data = new Data();

    string path;
    string fullPath;

    protected override void OnAwake()
    {
        path = $"{Application.dataPath}/Save/";
        fullPath = $"{path}Save.json";

        LoadData();
    }

    public void SaveData()
    {
        data.scores = scores;
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
            scores = loadedData.scores;
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
        for (int i = 0; i < 3; i++)
        {
            if (scores[i] < score)
            {
                for (int j = 3 - 1; j > i; j--)
                {
                    scores[j] = scores[j - 1];
                }
                scores[i] = score;

                break;
            }
        }
    }

    private void SetDefaultData()
    {
        for (int i = 0; i < 3; i++)
        {
            scores[i] = 0;
        }

        bgmVolume = 1;
        effectVolume = 1;
    }
}
