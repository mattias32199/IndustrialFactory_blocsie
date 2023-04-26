using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSystem : MonoBehaviour
{
    private List<RoboJoint> Joints = new List<RoboJoint>();
    public RoboJoint[] axes;
    public float DistanceThreshold;

    [SerializeField] private float LearningRate;
    [SerializeField] private float SamplingDistance;
    private float[] my_angles = new float[6];
    public GameObject target_object;


    public Vector3 ForwardKinematics (float[] angles) // indicates which point robotic arm is currently touching
    {
        Vector3 prevPoint = Joints[0].transform.position;
        Quaternion rotation = Quaternion.identity;
        for (int i = 1; i < Joints.Count; i++)
        {
            rotation *= Quaternion.AngleAxis(angles[i - 1], Joints[i - 1].Axis);
            Vector3 nextPoint = prevPoint + rotation * Joints[i].StartOffset;
            prevPoint = nextPoint;
        }
        return prevPoint;
    }

    public float DistanceFromTarget (Vector3 target, float [] angles)
    {
        Vector3 point = ForwardKinematics(angles);
        return Vector3.Distance(point, target);
    }

    public float PartialGradient (Vector3 target, float [] angles, int i)
    {
        float angle = angles[i];
        float f_x = DistanceFromTarget(target, angles);
        angles[i] += SamplingDistance;
        float f_x_plus_d = DistanceFromTarget(target, angles);
        
        float gradient = (f_x_plus_d - f_x) / SamplingDistance;
        //Debug.Log("f_x_plus_d: " + f_x_plus_d);
        //Debug.Log("f_x: " + f_x);
        //Debug.Log("Sampling distance: " + SamplingDistance);


        angles[i] = angle;

        return gradient;
    }

    public void InverseKinematics(Vector3 target, float[] angles)
    {
        if (DistanceFromTarget(target,angles) < DistanceThreshold)
        {
            return;
        }

        for (int i = Joints.Count - 1; i >= 0; i--)
        {
            float gradient = PartialGradient(target, angles, i);
            angles[i] -= LearningRate * gradient;

            if (DistanceFromTarget(target, angles) < DistanceThreshold)
            {
                return;
            }
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        Joints.AddRange(axes);
        my_angles[0] = Joints[0].GetAngle();
        my_angles[1] = Joints[1].GetAngle();
        my_angles[2] = Joints[2].GetAngle();
        my_angles[3] = Joints[3].GetAngle();
        my_angles[4] = Joints[4].GetAngle();
        my_angles[5] = Joints[5].GetAngle();

    }

    void Start()
    {
        //Vector3 target = target_object.transform.position;
        //InverseKinematics(target, angles);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < my_angles.Length; i++)
        {
            
        } 

        Debug.Log("sanity check");
        Vector3 my_target = target_object.transform.position;
        InverseKinematics(my_target, my_angles);

        Debug.Log("FK: " + ForwardKinematics(my_angles));
        Debug.Log("DistanceFromTarget: " + DistanceFromTarget(my_target, my_angles));


        //Debug.Log("sanitycheck1: " + DistanceFromTarget(my_target, my_angles));
        //Debug.Log("sanitycheck2: " + DistanceFromTarget(my_target, my_angles+0.2));

        for (int i = 0; i < Joints.Count - 1; i++)
        {
            Joints[i].MoveArm(my_angles[i]);
        }

    }
    }
