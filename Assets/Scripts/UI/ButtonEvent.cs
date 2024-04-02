using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public ButtonType type;

    Button button;

    public GameObject rulePanel;

    private void Awake()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        switch (type)
        {
            case ButtonType.GameStart:
                GameStart();
                break;
            case ButtonType.GameRule:
                GameRule();
                break;
            case ButtonType.GameQuit:
                GameQuit();
                break;
            case ButtonType.None:
            default:
                break;
        }
    }

    private void GameStart()
    {
        SceneHandler.Inst.NextSceneName = "GamePlayScene";
        GameManager.Inst.GameState = GameState.Play;
    }

    private void GameRule()
    {
        rulePanel.SetActive(true);
    }

    private void GameQuit()
    {
        Debug.Log("게임 종료 버튼 누름");
        Application.Quit();
    }
}
