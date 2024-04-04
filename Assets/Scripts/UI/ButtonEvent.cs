using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{
    public ButtonType type;

    Button button;

    public GameObject rulePanel;
    public GameObject settingPanel;

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
            case ButtonType.GameSetting:
                GameSetting();
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

    private void GameSetting()
    {
        settingPanel.SetActive(true);
    }

    private void GameQuit()
    {
        Application.Quit();
    }
}
