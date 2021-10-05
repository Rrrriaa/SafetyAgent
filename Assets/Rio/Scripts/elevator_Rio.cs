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
        print("------------충돌처리!!");
        if (gameObject.name == "UpDown")
        {
            //1층이면
            if (isDown)
            {
                print("------------충돌처리!!");
                //올라가는 애니메이션 실행
                player.transform.parent = elevator.transform;
                iTween.MoveTo(elevator, iTween.Hash("position", endPoint.position, "time", 10, "easeType", iTween.EaseType.easeOutQuart, "oncompleretarget",gameObject, "oncomplete","escape"));
            }
            else
            {
                //내려가는 애니메이션 실행
            }
        }
        if(gameObject.name == "CloseOpen")
        {
            //열려있으면
            if (isOpend)
            {
                //닫는 애니메이션 실행
                doorAnimator.SetBool("open",false);
            }
            //닫혀있으면
            else
            {
                //열리는 애니메이션 실행
                doorAnimator.SetBool("open", true);
            }
            
        }
    }
    void escape()
    {
        player.transform.parent = null;
    }
}
