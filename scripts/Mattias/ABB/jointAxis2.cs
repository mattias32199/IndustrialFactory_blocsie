using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// axis2, rotates up and down

public class jointAxis2 : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }
}
