using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    private Vector2 _delta; // measure position change
    private bool _isMoving; // movement switch
    private bool _isRotating; // rotating switch
    private float _xRotation; // amount to rotate
    [SerializeField] private float movementSpeed = 10.0f; // set movement speed in inspector
    [SerializeField] private float rotationSpeed = 0.5f; // set rotation speed in inspector

    private void Awake()
    {
        _xRotation = transform.rotation.eulerAngles.x;
    }

    public void OnLook(InputAction.CallbackContext context) // assign input to OnLook function
    {
        _delta = context.ReadValue<Vector2>(); // store position change
        //Debug.Log(_delta); 
    }
    public void OnMove(InputAction.CallbackContext context) // functions return move/rotate switch for LateUpdate check
    {
        _isMoving = context.started || context.performed; // based on InputSystem 
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        _isRotating = context.started || context.performed;
    }

    private void LateUpdate() // late update is called after every update
    {
        if (_isMoving)
        {
            var position = transform.right * (_delta.x * -movementSpeed); // calculate new position X
            position += transform.up * (_delta.y * -movementSpeed); // calculate new position Y
            transform.position += position * Time.deltaTime; // update current position
        }

        if (_isRotating)
        {
            transform.Rotate(new Vector3(_xRotation, _delta.x * rotationSpeed, 0.0f)); // assign new camera position
            transform.rotation = Quaternion.Euler(_xRotation, transform.rotation.eulerAngles.y, 0.0f); // rotate using local rotations
        }
    }
}