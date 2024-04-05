using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRulePanel : MonoBehaviour
{
    public Button[] buttons;

    private void Awake()
    {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(CloseButton);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach(Button button in buttons)
        {
            button.enabled = false;
        }
    }

    private void OnDisable()
    {
        foreach (Button button in buttons)
        {
            button.enabled = true;
        }
    }

    void CloseButton()
    {
        SoundManager.Inst.EffectSoundPlay(EffectTrack.Button);
        gameObject.SetActive(false);
    }
}
