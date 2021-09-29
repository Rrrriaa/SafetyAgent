using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHJ_Player : MonoBehaviour
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

        // lineR.SetPosition(0, 오른손위치);
        //lineR.SetPosition(1, 맞은위치);

    }

    // Update is called once per frame
    void Update()
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

    //public void DrawLine(Vector3 p1, Vector3 p2)
    //{
    //    //오른손 위치, 오른손 앞방향에서 나가는 Ray
    //    Ray ray = new Ray(trRight.position, trRight.forward);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        //부딫힌 위치를 잠시 저장
    //        movePoint = hit.point;
    //        //오른손위치, 부딫힌 위치까지 Line을 그린다
    //        lineR.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        lineR.gameObject.SetActive(false);
    //    }
    //}
}
