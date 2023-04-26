using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ConveyorBoxSpawner : MonoBehaviour
{
    // rigidbodyPrefab is the prefab we want to generate; it is public so we can drag any GameObject we want back in Unity
    public GameObject rigidbodyPrefab; // declare prefab
    GameObject Cube; // calls ball prefab

    // This integer is the random torque on each ball; it makes the physics less predictable and less organic
    public int RandomTorque = 0;

    // This float determines the space of time between generating each ball
    public float MakeCubeTimer = 1.5f; // float 1.5

    // How long each ball will last
    public int DestroyCubeTimer = 100;

    private int CubeStartSwitch; // switch to start InvokeRepeating
    public TMP_InputField BoxCycleInput; // conveyor cycle time
    public TMP_InputField BoxLifeCycleInput; // box life cycle time

    void Start()
    {
        //InvokeRepeating("MakeCube", 0.5f, MakeCubeTimer);
        CubeStartSwitch = 1;

    }
    void Update()
    {
        if ((Time.timeScale == 1) && (CubeStartSwitch == 1)) // CubeStartSwitch signifies start of simulation
        {
            string BoxCycleString = BoxCycleInput.text; // read input text field into string
            string BoxLifeString = BoxLifeCycleInput.text;

            float.TryParse(BoxCycleString, out MakeCubeTimer); // string to float
            int.TryParse(BoxLifeString, out DestroyCubeTimer); // string to int

            InvokeRepeating("MakeCube", 0.1f, MakeCubeTimer); // invoke repeat
            CubeStartSwitch = 0; // turn off switch
        }
    }

    void MakeCube()
    {
        Cube = Instantiate(rigidbodyPrefab, transform.position, transform.rotation) as GameObject; // create rigidbody based on prefab reference
        Cube.GetComponent<Rigidbody>().AddTorque(Random.Range(-RandomTorque, RandomTorque), Random.Range(-RandomTorque, RandomTorque), 0); // apply random torque
        Cube.transform.parent = transform; // asserts created object as child
        Destroy(Cube, DestroyCubeTimer); // destroys child object after DestroyCubeTimer has passed (seconds)
    }
}
