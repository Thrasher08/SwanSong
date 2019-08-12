using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{

    public float inputX;
    public float inputZ;
    public Vector3 moveDirection;

    public float rotationSpeed;
    public float speed;
    public float playerSpeed;
    public float playerRotation;

    public Camera cam;
    public CharacterController controller;
    public Animator anim;

    public bool blockRotation;
    public bool isGrounded;

    private float verticalVel;
    private Vector3 moveVector;


    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        cam = Camera.main;
        controller = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputMagnitude();

        //--Keeps Character Grounded--
        //CAN ALL BE REMOVED IF NECESSARY
        isGrounded = controller.isGrounded;

        if (isGrounded)
        {
            verticalVel -= 0;
        } else
        {
            verticalVel -= 2;
        }

        moveVector = new Vector3(0, verticalVel, 0).normalized;
        controller.Move(moveVector);
        //----------------------------
    }

    void PlayerMoveRotation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        moveDirection = forward * inputZ + right * inputX;

        if (!blockRotation)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), rotationSpeed);
            controller.Move(moveDirection * Time.deltaTime * playerSpeed);
        }
    }

    void InputMagnitude()
    {
        //Calculate the Input Vectors
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        anim.SetFloat("InputZ", inputZ, 0.0f, Time.deltaTime * 2f);
        anim.SetFloat("InputX", inputX, 0.0f, Time.deltaTime * 2f);

        //Calculate the Input Magnitude (how much input the player is making)
        speed = new Vector2(inputX, inputZ).normalized.sqrMagnitude;

        //Physically move player
        if (speed > playerRotation)
        {
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
            PlayerMoveRotation();
        } else if (speed < playerRotation){
            anim.SetFloat("InputMagnitude", speed, 0.0f, Time.deltaTime);
        }
    }
}
