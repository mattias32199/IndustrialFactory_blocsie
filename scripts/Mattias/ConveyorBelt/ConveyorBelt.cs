using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed; // conveyor belt speed, eventually make private
    Rigidbody rBody; // rigid body

    private Renderer rend; // struct type render
    private Vector2 scrollOffset; // direction texture moves: 2d vector

    [SerializeField] private bool powermode; // conveyorBelt power switch
    [SerializeField] private bool conveyor_direction; // conveyorBelt direction switch


    public void conveyorBelt_power() // change power status of conveyorBelt
    {
        if (powermode == false)
        {
            powermode = true;
        }
        else
        {
            powermode = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>(); // get rigidBody
        rend = GetComponent<Renderer>(); // texture render
        powermode = false; // start with power off
        
    }

    // Update is called once per frame
    void FixedUpdate() // FixedUpdate: updates based on physics system independent of framerate
    {
        Vector3 pos = rBody.position; // current position
        if (powermode == true) // check if conveyorBelt on
        {
            if (conveyor_direction == true) // check for direction
            {
                rBody.position -= Vector3.back * speed * Time.fixedDeltaTime; // modify current position

                scrollOffset = new Vector2((Time.time * speed * 0.59f), 0); // calculate texture offset
                rend.material.SetTextureOffset("_MainTex", -scrollOffset); // modify texture position

            }
            else
            {
                rBody.position += Vector3.back * speed * Time.fixedDeltaTime;

                scrollOffset = new Vector2((Time.time * speed * 0.59f), 0);
                rend.material.SetTextureOffset("_MainTex", scrollOffset);

            }
            rBody.MovePosition(pos); // "move" conveyorBelt
        }
    }
}
