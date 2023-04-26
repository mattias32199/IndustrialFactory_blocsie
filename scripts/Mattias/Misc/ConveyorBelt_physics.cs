using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt_physics : MonoBehaviour
{
    public float speed;
    Rigidbody rBody;

    public float textureSpeed = 0.5f;
    private Renderer rend; // struct type render
    private Vector2 scrollOffset; // direction texture moves: 2d vector

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scrollOffset = new Vector2(0, (-1 * Time.time * textureSpeed));
        rend.material.SetTextureOffset("_MainTex", scrollOffset);

        Vector3 pos = rBody.position;
        rBody.position += Vector3.back * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }
}
