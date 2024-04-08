using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameState gameState = GameState.None;
    // 이걸 어떻게 활용할까.... 고민중...
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
                    case GameState.GameOver:
                        onGameOver?.Invoke();
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
    public System.Action onGameOver;
    public System.Action<bool> onGamePause;

    protected override void OnAwake()
    {
        Application.targetFrameRate = 120;
    }

    protected override void Initialize()
    {
        onGamePlaying = null;
    }
}
