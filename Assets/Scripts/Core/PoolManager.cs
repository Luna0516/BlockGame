using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    CirclePool circlePool;
    SquarePool squarePool;
    TrianglePool trianglePool;

    protected override void OnAwake()
    {
        circlePool = GetComponentInChildren<CirclePool>();
        squarePool = GetComponentInChildren<SquarePool>();
        trianglePool = GetComponentInChildren<TrianglePool>();

        circlePool.Initialize();
        squarePool.Initialize();
        trianglePool.Initialize();
    }

    public GameObject GetObject(Shape shape)
    {
        GameObject obj = null;

        switch (shape)
        {
            case Shape.Circle:
                obj = circlePool.GetObject().gameObject;
                break;
            case Shape.Square:
                obj = squarePool.GetObject().gameObject;
                break;
            case Shape.Triangle:
                obj = trianglePool.GetObject().gameObject;
                break;
            case Shape.None:
            default:
                break;
        }

        if(obj == null)
        {
            Debug.LogError("반환하는 값이 NULL입니다!");
        }

        return obj;
    }

    public GameObject GetObject(Shape shape, Vector2 position)
    {
        GameObject obj = GetObject(shape);
        obj.transform.position = position;

        return obj;
    }
}
