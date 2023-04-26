using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;


public class Movement : MonoBehaviour
{
    public Animator playerAnim; // reference to the Animator component for the player object
    public Rigidbody playerRigid; // reference to the Rigidbody component for the player object
    public float w_speed, rn_speed, ro_speed, wobj_speed; // movement speeds for walking, running, rotating, and carrying objects
    public bool walk; // flag to track if the player is currently walking
    public bool walkobj; // flag to track if the player is currently walking while carrying an object
    public bool carryObj; // flag to track if the player is currently carrying an object
    public Transform playerTrans; // reference to the Transform component for the player object
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea; // reference to the Transform component for the area where the player can hold objects
    private GameObject heldObj; // reference to the game object that the player is currently holding
    private Rigidbody heldObjRb; // reference to the Rigidbody component for the game object that the player is currently holding

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange; // the range in which the player can pick up objects
    [SerializeField] private float pickupForce=2.7f; // the force at which the player can pick up objects

    private bool liftingOrDropping = false; // added variable to track if the player is lifting or dropping an object
    private bool liftUpstart = false; // added variable to start lift a little delayed to mathc the animation


    private float startTime = 0;// this is not used in final version

    void FixedUpdate()
    {
        if (liftingOrDropping == false ) // only update velocity if the player is not lifting or dropping an object
            
        {
            if (walkobj == true || walk == true)
            {
                playerRigid.velocity = transform.forward * wobj_speed * Time.deltaTime; // set the player's velocity to move forward with the specified speed
            }
        }
        else // set velocity to zero if the player is lifting or dropping an object
        {
            playerRigid.velocity = Vector3.zero;
        }
    }

    void Update()

    {
        if (Input.GetKeyDown(KeyCode.W) && carryObj == false && liftingOrDropping == false) // only start walking if the player is not lifting or dropping an object
        {
           
            playerAnim.SetTrigger("walk"); // set the "walk" trigger on the Animator component to play the walk animation
            playerAnim.ResetTrigger("idle"); // reset the "idle" trigger on the Animator component
           
            walk = true; // set the "walk" flag to true
        }


        if (Input.GetKeyUp(KeyCode.W) && carryObj == false && liftingOrDropping == false) // only stop walking if the player is not lifting or dropping an object
        {
            playerAnim.ResetTrigger("walk"); // reset the "walk" trigger on the Animator component
            playerAnim.SetTrigger("idle"); // set the "idle" trigger on the Animator component to play the idle animation
            walk = false; // set the "walk" flag to false
        }


        if (Input.GetKeyDown(KeyCode.W) && carryObj == true && liftingOrDropping == false) // only start walking with object if the player is not lifting or dropping an object
        {
            playerAnim.SetTrigger("walkobj"); // set the "walkobj" trigger on the Animator component to play the walk with object animation
            playerAnim.ResetTrigger("idlelift"); // reset the "idlelift" trigger on the Animator component
            walkobj = true; // set the "walkobj" flag to true
        }


        



        if (Input.GetKeyUp(KeyCode.W) && carryObj == true && liftingOrDropping == false) // only stop walking with object if the player is not lifting or dropping an object
        {
            playerAnim.ResetTrigger("walkobj");// set the "walkobj" trigger on the Animator component to play the walk with object animation
            playerAnim.SetTrigger("idlelift");// reset the "idlelift" trigger on the Animator component
            walkobj = false; // set the "walkobj" flag to false
        }






        if (Input.GetKey(KeyCode.W) == false) //if the key w is being pressed don't allow the object to be lifted at all
        {

            if (Input.GetKeyDown(KeyCode.F) && carryObj != true) // If the F key is pressed and the player is not carrying an object
            {


                Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickupRange); // Create an array of colliders in the area around the player
                foreach (var hitCollider in hitColliders) // Loop through all the colliders in the array

                {
                    if (hitCollider.gameObject.CompareTag("Box")) // If the collider is tagged as "Box"
                    {
                        liftingOrDropping = true;// Set the liftingOrDropping flag to true

                        playerAnim.SetTrigger("liftup"); // Trigger the lift-up animation
                        playerAnim.ResetTrigger("idle"); // Reset the idle animation trigge

                        PickupObject(hitCollider.gameObject, pickupForce); // Pick up the object and store it in heldObj
                                                                           



                        break; // Exit the loop
                    }
                }
            }




      
            if (Input.GetKeyDown(KeyCode.C) && carryObj == true) // If the C key is pressed and the player is carrying an object
            {
               liftingOrDropping = true; // Set the liftingOrDropping flag to true

                playerAnim.SetTrigger("liftdown"); // Trigger the lift-down animation

                playerAnim.ResetTrigger("idlelift"); // Reset the idlelift animation trigger



                DropObject(); // Drop the object
            }


        }



        if (Input.GetKey(KeyCode.A) && liftingOrDropping==false) // If the A key is held down and rotation disabled while lifting animation occurs 
        {
            playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0); // Rotate the player to the left
        }

        if (Input.GetKey(KeyCode.D) && liftingOrDropping == false) // If the D key is held down and rotation disabled while lifting animation occurs 
        {
            playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0); // Rotate the player to the right
        }

        void PickupObject(GameObject pickObj, float liftForce)
        {
            if (pickObj.GetComponent<Rigidbody>())
            { // If the object has a Rigidbody component
                heldObjRb = pickObj.GetComponent<Rigidbody>(); // Store the object's Rigidbody component in heldObjRb
                heldObjRb.useGravity = false; // Turn off gravity for the object
                heldObjRb.drag = 6; // Set the object's drag to 0
                heldObjRb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze the object's rotation

                heldObjRb.transform.parent = holdArea; // Set the object's parent to the hold area
                heldObj = pickObj; // Store the object in heldObj

                carryObj = true; // Set the carryObj flag to true

                // Apply a force to lift the object to the hold area
                Vector3 liftDirection = (holdArea.position - pickObj.transform.position).normalized;

                //heldObjRb.AddForce(liftDirection * pickupForce, ForceMode.Impulse);
                StartCoroutine(DelayedAddForce(liftDirection, pickupForce, 1.0f)); // Call the coroutine with the desired delay time
            }
        }



        void DropObject()
        {
            if (heldObj != null)
            {
                heldObjRb.useGravity = true; // Enable gravity on the held object
                heldObjRb.drag = 10; // Set the drag of the held object
                heldObjRb.constraints = RigidbodyConstraints.None; // Remove any constraints on the held object's rigidbody
                heldObjRb.transform.parent = null; // Detach the held object from the hold area
                heldObj = null; // Reset the held object to null 
                carryObj = false; // Set the carryObj flag to false to indicate that the player is not carrying an object

                
            }

           
        }
    }

        public void EndLiftAnimation()
        {
            liftingOrDropping = false; // Set the liftingOrDropping flag to false to indicate that the lift/drop animation has ended
        }

       public void StarttheliftUp()
        {
            liftUpstart = false; // Set the liftUpstart flag to false to indicate that the lift/drop animation has ended
        }


 
    IEnumerator DelayedAddForce(Vector3 liftDirection, float pickupForce, float delaySeconds)// A coroutine that adds a force to the held object after a delay
    {
        
        yield return new WaitForSeconds(delaySeconds);  // Wait for the specified amount of time

       
        heldObjRb.AddForce(liftDirection * pickupForce, ForceMode.Impulse);  // Add the force to the rigidbody of the held object
    }

}


