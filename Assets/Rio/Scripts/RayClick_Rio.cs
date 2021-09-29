using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayClick_Rio : MonoBehaviour
{
    //오른손
    public Transform trRight;
    //가리킬 위치
    Vector3 hitPoint;

    public LineRenderer line;

    private void Update()
    {
        Ray ray = new Ray(trRight.position, trRight.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //오른손 위치, 부딪힌 위치까지 line을 그린다
            line.SetPosition(0, trRight.position);
            line.SetPosition(1, hit.point);


            //버튼 컴포넌트 가져오기
            Button btn = hit.transform.GetComponent<Button>();

            //충돌한게 버튼이 맞다면
            if (btn != null)
            {
                print("버튼 발견!!");
                //버튼 클릭하면 이벤트 발생
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                {
                    print("버튼 클릭!!");
                    //버튼 onclick에 등록된 함수를 실행
                    btn.onClick.Invoke();
                }
            }
        }
        
    }
}
