using UnityEngine;


[RequireComponent(typeof(CharacterController))] //przypisanie obiektowi CharacterController
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 3f;
    public float playerSpeedRun = 12f;
    public float jump = 18f;
    public float upDownRotation = 0;
    public Camera camera;
    public float rotationLimiter = 120;
    private float jumpSpeed;
    CharacterController characterController;
    private float forwardKey;
    private float sidewaysKey;
    
    void Awake() //pobranie CharacterController
    {
        characterController = GetComponent<CharacterController>();
        Cursor.visible = false; //wyłączenie widoczności kursora
        Cursor.lockState = CursorLockMode.Locked; //zablokowanie kursora na środku ekranu
    }
    void Update()
    {
        float leftRightRotation = Input.GetAxis("Mouse X");
        transform.Rotate(0, leftRightRotation, 0);
         upDownRotation -= Input.GetAxis("Mouse Y");
        upDownRotation = Mathf.Clamp(upDownRotation, -rotationLimiter, rotationLimiter);//ogranieczenie poruszania góra-dół
        camera.transform.localRotation = Quaternion.Euler(upDownRotation, 0, 0);
        //chodzenie
        if (characterController.isGrounded)
        {
            forwardKey = Input.GetAxis("Vertical") * playerSpeed;
            sidewaysKey = Input.GetAxis("Horizontal") * playerSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                forwardKey = Input.GetAxis("Vertical") * playerSpeedRun;
                sidewaysKey = Input.GetAxis("Horizontal") * playerSpeedRun;
            }
        }
        jumpSpeed += Physics.gravity.y * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {   
            jumpSpeed = jump;
        }
        Vector3 playerKeys = new Vector3(sidewaysKey, jumpSpeed, forwardKey);
        characterController.Move(transform.rotation *playerKeys * Time.deltaTime);

    }
}
