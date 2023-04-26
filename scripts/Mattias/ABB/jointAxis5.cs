using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// axis5, rotates around z-axis
public class jointAxis5 : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }
}
