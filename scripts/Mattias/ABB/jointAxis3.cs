using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// axis3, rotates up and down

public class jointAxis3 : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }
}
