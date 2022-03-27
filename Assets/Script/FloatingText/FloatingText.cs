using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private int DestroyTime = 4;
    private Vector3 Offset = new Vector3(0, 4, 0);
    private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);

    void Start()
    {
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x), Random.Range(-randomizeIntensity.y, randomizeIntensity.y), Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
        Destroy(gameObject, DestroyTime);

    }

}
