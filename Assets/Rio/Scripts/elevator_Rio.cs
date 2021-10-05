using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator_Rio : MonoBehaviour
{
    public Animator doorAnimator;
    //public Animator eleveAnimator;
    public GameObject player;
    public Transform startPoint;
    public Transform endPoint;
    public GameObject elevator;

    bool isOpend = true;
    bool isDown = true;

    private void OnTriggerEnter(Collider other)
    {
        print("------------�浹ó��!!");
        if (gameObject.name == "UpDown")
        {
            //1���̸�
            if (isDown)
            {
                print("------------�浹ó��!!");
                //�ö󰡴� �ִϸ��̼� ����
                player.transform.parent = elevator.transform;
                iTween.MoveTo(elevator, iTween.Hash("position", endPoint.position, "time", 10, "easeType", iTween.EaseType.easeOutQuart, "oncompleretarget",gameObject, "oncomplete","escape"));
            }
            else
            {
                //�������� �ִϸ��̼� ����
            }
        }
        if(gameObject.name == "CloseOpen")
        {
            //����������
            if (isOpend)
            {
                //�ݴ� �ִϸ��̼� ����
                doorAnimator.SetBool("open",false);
            }
            //����������
            else
            {
                //������ �ִϸ��̼� ����
                doorAnimator.SetBool("open", true);
            }
            
        }
    }
    void escape()
    {
        player.transform.parent = null;
    }
}
