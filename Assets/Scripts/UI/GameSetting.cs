using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : MonoBehaviour
{
    public Button[] buttons;

    public Slider bgmSlider;
    public Slider effectSlider;

    private void Awake()
    {
        Button button = GetComponentInChildren<Button>();
        button.onClick.AddListener(CloseButton);

        bgmSlider.value = SoundManager.Inst.bgm.volume * 2;
        bgmSlider.onValueChanged.AddListener(SoundManager.Inst.OnBGMVolumeChanged);

        effectSlider.value = SoundManager.Inst.effect.volume * 2;
        effectSlider.onValueChanged.AddListener(SoundManager.Inst.OnEffectVolumeChanged);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        foreach (Button button in buttons)
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
