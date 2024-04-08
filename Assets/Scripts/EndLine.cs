using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    bool gameOver = false;

    float elapsedTime = 0.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Block block = collision.GetComponent<Block>();
            if (block.isActive)
            {
                elapsedTime += Time.deltaTime;
            }

            if(elapsedTime > 0.5f && !gameOver)
            {
                gameOver = true;
                GameManager.Inst.GameState = GameState.GameOver;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            elapsedTime = 0.0f;
        }
    }
}
