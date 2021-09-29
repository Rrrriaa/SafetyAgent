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

        // lineR.SetPosition(0, ��������ġ);
        //lineR.SetPosition(1, ������ġ);

    }

    // Update is called once per frame
    void Update()
    {
        //���̽�ƽ �� �޾ƿ��� x �¿� ,y ����
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
    //    //������ ��ġ, ������ �չ��⿡�� ������ Ray
    //    Ray ray = new Ray(trRight.position, trRight.forward);
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray, out hit))
    //    {
    //        //�΋H�� ��ġ�� ��� ����
    //        movePoint = hit.point;
    //        //��������ġ, �΋H�� ��ġ���� Line�� �׸���
    //        lineR.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        lineR.gameObject.SetActive(false);
    //    }
    //}
}
