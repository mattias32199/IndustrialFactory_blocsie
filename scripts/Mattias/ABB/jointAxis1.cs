using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// axis1, rotates around y-axis
// (base joint)

public class jointAxis1 : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }
}
