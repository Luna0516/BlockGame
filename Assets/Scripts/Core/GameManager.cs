using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void OnAwake()
    {
        Application.targetFrameRate = 120;
    }
}
