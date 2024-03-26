using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePool : Pool<Block>
{
    protected override void OnGenerateObjects(Block comp)
    {
        comp.shape = Shape.Triangle;
    }
}
