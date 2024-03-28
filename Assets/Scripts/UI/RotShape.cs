using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotShape : MonoBehaviour
{
    float rotSpeed;

    private void Awake()
    {
        rotSpeed = Random.Range(-45.0f, 45.0f);

        transform.localScale = Vector3.one * Random.Range(1.0f, 2.5f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * rotSpeed);
    }
}
