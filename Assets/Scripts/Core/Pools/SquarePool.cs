using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePool : Pool<Block>
{
    protected override void OnGenerateObjects(Block comp)
    {
        comp.shape = Shape.Square;
    }
}