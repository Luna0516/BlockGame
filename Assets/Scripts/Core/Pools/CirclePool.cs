using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePool : Pool<Block>
{
    protected override void OnGenerateObjects(Block comp)
    {
        comp.shape = Shape.Circle;
    }
}