using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// joint class

public class RoboJoint : MonoBehaviour
{
    public Vector3 Axis;
    public Vector3 StartOffset;
    public Vector3 ZeroEuler;

    public float MinAngle;
    public float MaxAngle;

    void Awake()
    {
        StartOffset = transform.localPosition;
        ZeroEuler = transform.localEulerAngles;
    }

    /////////////////////////////////////////////////////////////////
    
    public float ClampAngle(float angle, float delta = 0)
    {
        return Mathf.Clamp(angle + delta, MinAngle, MaxAngle);
    }

    // Get the current angle
    public float GetAngle()
    {
        float angle = 0;
        if (Axis.x == 1) angle = transform.localEulerAngles.x;
        else
        if (Axis.y == 1) angle = transform.localEulerAngles.y;
        else
        if (Axis.z == 1) angle = transform.localEulerAngles.z;

        return ClampAngle(angle);
    }
    public float SetAngle(float angle)
    {
        angle = ClampAngle(angle);
        if (Axis.x == 1) transform.localEulerAngles = new Vector3(angle, 0, 0);
        else
        if (Axis.y == 1) transform.localEulerAngles = new Vector3(0, angle, 0);
        else
        if (Axis.z == 1) transform.localEulerAngles = new Vector3(0, 0, angle);

        return angle;
    }



    // Moves the angle to reach 
    public float MoveArm(float angle)
    {
        return SetAngle(angle);
    }
}
