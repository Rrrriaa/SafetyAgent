using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove_Rio : MonoBehaviour
{
    CharacterController controller;

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;

   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

      
    }

   
    // Update is called once per frame
    void Update()
    {
       
        playerMove();
        playerRot();
    }

    void playerMove()
    {

        //조이스틱 값 받아오기 x 좌우 ,y 상하
        Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
                
        moveDirection = new Vector3(stickPos.x, 0, stickPos.y);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        if (Input.GetButton("Jump"))
            moveDirection.y = jumpSpeed;
        
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


    void playerRot()
    {
        if (!LiftSceneMngr.instance.isRide)
        {
            transform.rotation = Quaternion.Euler(rot);
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickRight, OVRInput.Controller.RTouch))
            {
                rotPlus();
            }
            if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickLeft, OVRInput.Controller.RTouch))
            {
                rotMinus();
            }
        }
    }

    Vector3 rot;
    void rotPlus()
    {
        rot.y += 45;
    }
    void rotMinus()
    {
        rot.y -= 45;
    }

}
