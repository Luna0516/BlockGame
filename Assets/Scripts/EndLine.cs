using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
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

            if(elapsedTime > 0.5f)
            {
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
