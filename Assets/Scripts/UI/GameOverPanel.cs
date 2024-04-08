using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    float activeSpeed = 2.0f;

    CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        CanvasGroup(false);

        GameManager.Inst.onGameOver += () =>
        {
            CanvasGroup(true);
        };
    }

    void CanvasGroup(bool active)
    {
        if (active)
        {
            StartCoroutine(StartActive());
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = active;
            canvasGroup.blocksRaycasts = active;
        }
    }

    IEnumerator StartActive()
    {
        float elapsed = 0.0f;

        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if(elapsed > 1) { break; }

            elapsed += Time.deltaTime * activeSpeed;

            canvasGroup.alpha = elapsed;

            yield return null;
        }

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
}
