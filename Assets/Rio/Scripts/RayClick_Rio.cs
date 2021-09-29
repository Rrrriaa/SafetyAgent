using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayClick_Rio : MonoBehaviour
{
    //������
    public Transform trRight;
    //����ų ��ġ
    Vector3 hitPoint;

    public LineRenderer line;

    private void Update()
    {
        Ray ray = new Ray(trRight.position, trRight.forward);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //������ ��ġ, �ε��� ��ġ���� line�� �׸���
            line.SetPosition(0, trRight.position);
            line.SetPosition(1, hit.point);


            //��ư ������Ʈ ��������
            Button btn = hit.transform.GetComponent<Button>();

            //�浹�Ѱ� ��ư�� �´ٸ�
            if (btn != null)
            {
                print("��ư �߰�!!");
                //��ư Ŭ���ϸ� �̺�Ʈ �߻�
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                {
                    print("��ư Ŭ��!!");
                    //��ư onclick�� ��ϵ� �Լ��� ����
                    btn.onClick.Invoke();
                }
            }
        }
        
    }
}
