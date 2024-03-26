using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : MonoBehaviour where T : PoolObject
{
    public GameObject origianlPrefab;

    public int poolSize = 64;

    T[] pool;

    Queue<T> readyQueue;

    public void Initialize()
    {
        if(pool == null)
        { 
            pool = new T[poolSize];
            readyQueue = new Queue<T>(poolSize);

            GenerateObjects(0, poolSize, pool);
        }
        else
        {
            foreach(T obj in pool)
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    public T GetObject(Transform spawnTransform = null)
    {
        if (readyQueue.Count > 0)
        {
            T comp = readyQueue.Dequeue();
            if(spawnTransform != null)
            {
                comp.transform.SetPositionAndRotation(
                    spawnTransform.position, spawnTransform.rotation);                
                comp.transform.localScale = spawnTransform.localScale;
            }
            else
            {             
                comp.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                comp.transform.localScale = Vector3.one;
            }
            comp.gameObject.SetActive(true);
            return comp;
        }
        else
        {
            ExpandPool();
            return GetObject(spawnTransform);
        }
    }

    private void ExpandPool()
    {
        Debug.LogWarning($"{gameObject.name} 풀 사이즈 증가. {poolSize} -> {poolSize * 2}");

        int newSize = poolSize * 2;
        T[] newPool = new T[newSize];
        for(int i=0;i<poolSize;i++)
        {
            newPool[i] = pool[i];
        }

        GenerateObjects(poolSize, newSize, newPool);
        pool = newPool;
        poolSize = newSize;
    }

    private void GenerateObjects(int start, int end, T[] newArray)
    {
        for (int i = start; i < end; i++)
        {
            GameObject obj = Instantiate(origianlPrefab, transform);
            obj.name = $"{origianlPrefab.name}_{i}";

            T comp = obj.GetComponent<T>();
            comp.onDisable += () => readyQueue.Enqueue(comp);

            OnGenerateObjects(comp);

            newArray[i] = comp;
            obj.SetActive(false);
        }
    }

    protected virtual void OnGenerateObjects(T comp) { }
}
