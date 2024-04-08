using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button closeButton;

    public Slider bgmSlider;
    public Slider effectSlider;
    public Slider playerMoveSpeedSlider;

    public TextMeshProUGUI scoreText;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        closeButton.onClick.AddListener(CloseButton);

        bgmSlider.value = SoundManager.Inst.bgm.volume * 2;
        bgmSlider.onValueChanged.AddListener(SoundManager.Inst.OnBGMVolumeChanged);

        effectSlider.value = SoundManager.Inst.effect.volume * 2;
        effectSlider.onValueChanged.AddListener(SoundManager.Inst.OnEffectVolumeChanged);

        GameManager.Inst.onGamePause = null;
        GameManager.Inst.onGamePause += CanvasGroup;
    }

    private void Start()
    {
        CanvasGroup(false);
    }

    void CloseButton()
    {
        SoundManager.Inst.EffectSoundPlay(EffectTrack.Button);
        GameManager.Inst.Player.isPause = false;
        GameManager.Inst.onGamePause.Invoke(false);
    }

    void CanvasGroup(bool active)
    {
        if (active)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
    }
}
