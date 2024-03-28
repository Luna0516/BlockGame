using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState = GameState.None;
    public GameState GameState
    {
        get => gameState;
        set
        {
            if(gameState != value)
            {
                gameState = value;

                switch (gameState)
                {
                    case GameState.Execution:
                        break;
                    case GameState.Start:
                        break;
                    case GameState.Play:
                        break;
                    case GameState.None:
                    default:
                        break;
                }
            }
        }
    }

    private Player player;
    public Player Player
    {
        get
        {
            if(player == null)
            {
                player = FindObjectOfType<Player>();
            }

            return player;
        }
    }

    public System.Action onGamePlaying;

    protected override void OnAwake()
    {
        Application.targetFrameRate = 120;
    }

    protected override void Initialize()
    {
        onGamePlaying = null;
    }
}
