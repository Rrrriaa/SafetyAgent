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

        //현재 게임오브젝트의 각도를 mx.my에 셋팅
        mx = transform.localEulerAngles.x;
        my = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
    }

    void playerMove()
    {

        //조이스틱 값 받아오기 x 좌우 ,y 상하
        Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(stickPos.x, 0, stickPos.y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }


    float mx;
    float my;
    public float rotSpeed = 200;

    void playerRot()
    { 
       


    }

}
