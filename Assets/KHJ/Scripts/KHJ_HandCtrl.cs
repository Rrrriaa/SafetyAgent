using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHJ_HandCtrl : MonoBehaviour
{

    //������ Transform
    public Transform trRight;
    //�޼� Transform
    public Transform trLeft;

    //���� ��ü�� Transform
    Transform trCatchedR;
    Transform trCatchedL;

    public float throwPower = 5;

    void Start()
    {

    }

    void Update()
    {
        CatchObj();
        DropObj();

        if(trCatchedR.gameObject.name == "Helmet" || trCatchedR.gameObject.name == "WelderMask")
        {
            trCatchedR.GetComponent<KHJ_Item>().DisappearItem();

        }


    }

    void CatchObj()
    {
        //1. ���࿡ ������ ��Ʈ�ѷ��� �׷���ư�� ������
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            //2. ��������ġ, �����վչ��⿡�� ������ Ray�� �����
            Ray ray = new Ray(trRight.position, trRight.forward);
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCastAll
            RaycastHit[] hits = Physics.SphereCastAll(ray, 0.5f, 1, layer);
            //���࿡ �΋H���� �ִٸ�
            if (hits.Length > 0)
            {
                //4.�ε��� ��ü�� ���� ����� ��ü�� ��´�. (�ε��� ��ü�� �������� �ڽ����� �Ѵ�)
                hits[0].transform.parent = trRight;
                //5. ���� ��ü�� trCatched�� �־�д�
                trCatchedR = hits[0].transform;
                //Item������Ʈ�� isGrab�� true�� ������ش�
                trCatchedR.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true);
            }
            #endregion
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            //2. �޼���ġ, �޼վչ��⿡�� ������ Ray�� �����
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCastAll
            RaycastHit[] hits = Physics.SphereCastAll(ray, 0.5f, 1, layer);
            //���࿡ �΋H���� �ִٸ�
            if (hits.Length > 0)
            {
                //4.�ε��� ��ü�� ���� ����� ��ü�� ��´�. (�ε��� ��ü�� �������� �ڽ����� �Ѵ�)
                hits[0].transform.parent = trLeft;
                //5. ���� ��ü�� trCatched�� �־�д�
                trCatchedL = hits[0].transform;
                //Item������Ʈ�� isGrab�� true�� ������ش�
                trCatchedL.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true);
            }
            #endregion
        }

    }

    void DropObj()
    {
        //1. ���࿡ ������ �׷���ư�� ������
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            if (trCatchedR != null)
            {
                Throwobj();
                //2. ���� ��ü�� ���´�.(���� ��ü�� �θ� ���ش�)
                trCatchedR.parent = null;
                //3. trCatced�� ���� null�� �Ѵ�
                trCatchedR = null;
                //Item������Ʈ�� isGrab�� false�� ������ش�
                trCatchedR.GetComponent<KHJ_Item>().isGrab = false;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            if (trCatchedL != null)
            {
                Throwobj();
                //2. ���� ��ü�� ���´�.(���� ��ü�� �θ� ���ش�)
                trCatchedL.parent = null;
                //3. trCatced�� ���� null�� �Ѵ�
                trCatchedL = null;
                //Item������Ʈ�� isGrab�� false�� ������ش�
                trCatchedL.GetComponent<KHJ_Item>().isGrab = false;
            }
        }
    }

    void Throwobj()
    {
        SetKinematic(false);
        //������ ���� (�̵��ӷ�)
        Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        // ������ ȸ������ (ȸ���ӷ�)
        Vector3 angularDir = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
        //���� ��ü�� �پ��ִ� RIgidbody�� ��������
        Rigidbody rb = trCatchedR.GetComponent<Rigidbody>();
        //Velocity ���� dir�� ����
        rb.velocity = dir * throwPower;
        // angularVelocity�� angulardir�� ����
        rb.angularVelocity = angularDir;
    }

    void SetKinematic(bool enable)
    {
        // ���� ��ü���� Rigidbody ������Ʈ �����´�
        Rigidbody rb = trCatchedR.GetComponent<Rigidbody>();
        // ������ ������Ʈ�� isKinematic�� false�Ѵ�
        rb.isKinematic = enable;
        //true �ϸ� ����X false�ϸ� ����O

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(trRight.position, 1);
        Gizmos.DrawSphere(trLeft.position, 1);
    }
}
