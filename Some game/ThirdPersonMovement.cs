using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f; // for the smoothness of player rotation
    float turnSmoothVelocity;
    private Animator anim;
    private float moveSpeed;
    private float walkSpeed;
    private float runSpeed;

    void Start(){
        anim = GetComponentInChildren<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //GetAxis adds smooothness to the movements
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized; //store your direction values and prevent character from moving upwards (y) ,
                                                                              //normalised helps normalise the speed when holding down 2 keys of different directions
        
        if (direction.magnitude >= 0.1f)//checks if we are moving in any direction
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)* Mathf.Rad2Deg + cam.eulerAngles.y;  // helps rotate the player to the direction hes moving ,
                                                                                       // rad2deg changes the radian value to degree 
                                                                                       // put in xy values to get back degrees , assuming start looking point is 0 degrees (starting point)
                                                                        // xy graph , every rotate is like small cut on the graph when player rotates ,
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);   // def a way to smoothen out rotateions with fine    
                                                                                                                                 //calculations 
            transform.rotation = Quaternion.Euler(0f, angle, 0f) ; // and now the player rotates
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward; // to make the player travel the direction that the camera is facing ,
                                                                                       // meaning point right or left properly from where we are looking at the screen 
                                                                                                                          
            controller.Move(moveDir.normalized * speed * Time.deltaTime); //finalised movement and speed
        }
    }
    private void Idle() {
        anim.SetFloat("Speed", 0);
            }
    private void Walk() {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f);
    }
    private void Run() {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1);
    }
    private void Jump() { }
}
