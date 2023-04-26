using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMaker : MonoBehaviour
{
    // rigidbodyPrefab is the prefab we want to generate; it is public so we can drag any GameObject we want back in Unity
    public GameObject rigidbodyPrefab; // declare prefab
    GameObject Cube; // calls ball prefab

    // This integer is the random torque on each ball; it makes the physics less predictable and less organic
    public int RandomTorque = 15;

    // This float determines the space of time between generating each ball
    public float MakeCubeTimer = 1.5f; // 1.5 frames?

    // How long each ball will last
    public int DestroyCubeTimer = 100;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("MakeCube", 0.5f, MakeCubeTimer);

    }

    void MakeCube()
    {
        Cube = Instantiate(rigidbodyPrefab, transform.position, transform.rotation) as GameObject;
        Cube.GetComponent<Rigidbody>().AddTorque(Random.Range(-RandomTorque, RandomTorque), Random.Range(-RandomTorque, RandomTorque), 0);
        Cube.transform.parent = transform;
        Destroy(Cube, DestroyCubeTimer);
    }
}
