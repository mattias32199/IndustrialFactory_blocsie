using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// axis6, rotates around y-axis
public class jointAxis6 : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;

    void Awake()
    {
        StartOffset = transform.localPosition;
    }
}
