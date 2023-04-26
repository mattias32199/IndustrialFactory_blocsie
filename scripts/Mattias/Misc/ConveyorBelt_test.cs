using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt_test : MonoBehaviour {

    // Scroll main texture based on time
    public float textureSpeed = 0.5f;
    public float conveyorForce = 1;

    private Renderer rend; // struct type render
    private Vector2 scrollOffset; // direction texture moves: 2d vector

    void Start() {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        scrollOffset = new Vector2(0, (Time.time * textureSpeed));
        rend.material.SetTextureOffset("_MainTex", scrollOffset);
    }

    private void OnCollisionStay(Collision collider) {

        Vector3 conveyorCalculatedForce = ((conveyorForce * 100) * Time.deltaTime) * new Vector3(transform.forward.x, transform.forward.y, transform.forward.z);
        collider.rigidbody.AddForce(conveyorCalculatedForce, ForceMode.Acceleration);
    }
}
