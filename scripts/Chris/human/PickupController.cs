using UnityEngine;

public class PickupController : MonoBehaviour
{


    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRb;


    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange;

    [SerializeField] private float pickupForce;
    


    private void Update()


    {
        
               

            }



      

    



   




    void PickupObject(GameObject pickObj)
    {

        if (pickObj.GetComponent<Rigidbody>())
        {


            heldObjRb = pickObj.GetComponent<Rigidbody>();
            heldObjRb.useGravity = false;
            heldObjRb.drag = 10;
            heldObjRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRb.transform.parent = holdArea;
            heldObj = pickObj;
        }

    }



   
}