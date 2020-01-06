using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Rigidbody rb;

    float speed = 4;
    float rotSpeed = 80;
    float gravity = 8;
    float rot = 0f;
    //Animator animator;
    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();    //referencira se na playera
        //animator = GetComponent<Animator>();                    
        rb = GetComponent<Rigidbody>();
    }

    //Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets rotation vector for camera
    public void RotateCamera(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }

    //Run every physics iteration
    void FixedUpdate()
    {

        //GetInput();
        //PerformAttack();
        if (!isLocalPlayer)
        {
            return;
        }
        PerformMovement();
        if (isServer)
        {
            RpcPerformMovement();
        }
        else
        {
            CmdPerformMovement();
        }
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        //if (controller.isGrounded)
        //{
        //    if (Input.GetKey(KeyCode.W))
        //{
        //    //animator.SetBool("Moving", true);
        //    moveDirection = new Vector3(0, 0, 1);
        //    moveDirection *= speed;
        //    moveDirection = transform.TransformDirection(moveDirection);
        //}
        //if (Input.GetKeyUp(KeyCode.W))
        //{
        //    //animator.SetBool("Moving", false);
        //    moveDirection = new Vector3(0, 0, 0);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    //animator.SetBool("Moving", true);
        //    moveDirection = new Vector3(0, 0, 1);
        //    moveDirection *= speed;
        //    moveDirection = transform.TransformDirection(-moveDirection);
        //}
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    //animator.SetBool("Moving", false);
        //    moveDirection = new Vector3(0, 0, 0);
        //}
        //}
        //rot += Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        //transform.eulerAngles = new Vector3(0, rot, 0);
        //moveDirection.y -= gravity * Time.deltaTime;
        //controller.Move(moveDirection * Time.deltaTime);

        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

    [Command]
    void CmdPerformMovement()
    {
        PerformMovement();
        RpcPerformMovement();
    }

    [ClientRpc]
    void RpcPerformMovement()
    {
        if (isLocalPlayer)
        {
            return;
        }
        PerformMovement();
    }

    //void PerformAttack()
    //{
    //    animator.SetTrigger(1);
    //}

    //void GetInput()
    //{
    //   if (Input.GetButtonDown("Fire1"))
    //   {

    //        if (animator.GetBool("Moving"))
    //        {
    //            animator.SetBool("Moving", false);
    //        }
    //        else
    //        {
    //            PerformAttack();
    //        }
    //        PerformAttack();
    //   }
    //   //animator.SetBool("Attack", false);
    //}
}
