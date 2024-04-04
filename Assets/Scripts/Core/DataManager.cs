using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [Range(0f, 1f)]
    public float bgmVolume = 1;

    Data data = null;
}
