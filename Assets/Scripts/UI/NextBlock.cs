using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBlock : MonoBehaviour
{
    Image image;

    public Sprite[] circleSprites;
    public Sprite[] squareSprites;
    public Sprite[] triangleSprites;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        GameManager.Inst.Player.onSetNextBlock += (block) =>
        {
            Shape shape = block.shape;
            int level = block.level;

            Sprite sprite = null;
            switch (shape)
            {
                case Shape.Circle:
                    sprite = circleSprites[level];
                    break;
                case Shape.Square:
                    sprite = squareSprites[level];
                    break;
                case Shape.Triangle:
                    sprite = triangleSprites[level];
                    break;
                case Shape.None:
                default:
                    break;
            }
            ShowNextBlock(sprite);
        };
    }

    private void ShowNextBlock(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
