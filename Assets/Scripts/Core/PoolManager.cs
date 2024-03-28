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
                obj = circlePool.GetObject(false).gameObject;
                break;
            case Shape.Square:
                obj = squarePool.GetObject(false).gameObject;
                break;
            case Shape.Triangle:
                obj = trianglePool.GetObject(false).gameObject;
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

    public Block GetBlock(Shape shape, Vector2 position)
    {
        GameObject obj = GetObject(shape);
        obj.transform.position = position;

        Block block = obj.GetComponent<Block>();

        return block;
    }

    public GameObject GetRandomObject(Vector2 position)
    {
        int rand = Random.Range(1, 4);

        GameObject obj = GetObject((Shape)rand);
        obj.SetActive(true);
        obj.transform.position = position;

        Block block = obj.GetComponent<Block>();
        block.Level = Random.Range(0, 5);
        block.Active();

        StartCoroutine(LifeTime(obj));

        return obj;
    }

    IEnumerator LifeTime(GameObject obj)
    {
        yield return new WaitForSeconds(5.0f);

        obj.SetActive(false);
    }
}
