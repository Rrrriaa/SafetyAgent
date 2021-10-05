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
    public Transform trCatchedR;
    public Transform trCatchedL;

    public float throwPower = 5;


    public GameObject HelpMan;


    void Update()
    {
        CatchObj();
        DropObj();

        if (OVRInput.GetDown(OVRInput.RawButton.Y, OVRInput.Controller.LTouch))
        {
            //Next
            HelpMan.GetComponent<HelpMan>().DisplayNextSentence();
        }
        if (OVRInput.GetDown(OVRInput.RawButton.X, OVRInput.Controller.LTouch))
        {
            //Pre
            HelpMan.GetComponent<HelpMan>().DisplayPreSentence();
        }


    }

    void CatchObj()
    {
        //1. ���࿡ ������ ��Ʈ�ѷ��� �׷���ư�� ������
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            //2. ��������ġ, �����վչ��⿡�� ������ Ray�� �����
            Ray ray = new Ray(trRight.position, trRight.forward);
            
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCast
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.1f, out hit, 1, layer))
            {
                if(hit.transform.gameObject.tag == "Tarp")
                {
                    hit.transform.gameObject.GetComponent<Animator>().SetTrigger("Move");
                    return;
                }
                //4. �ε��� ��ü�� ��´�. (�ε��� ��ü�� �������� �ڽ����� �Ѵ�)
                hit.transform.parent = trRight;
                hit.transform.position = trRight.position;
                hit.transform.rotation = trRight.rotation;
                //5. ���� ��ü�� trcatched�� �־�д�
                trCatchedR = hit.transform;
                trCatchedR.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true, trCatchedR);
            }
            #endregion
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //2. �޼���ġ, �޼վչ��⿡�� ������ Ray�� �����
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCast
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.1f, out hit, 1, layer))
            {
                //4. �ε��� ��ü�� ��´�. (�ε��� ��ü�� �޼��� �ڽ����� �Ѵ�)
                hit.transform.parent = trLeft;
                hit.transform.position = trLeft.position;
                hit.transform.rotation = trLeft.rotation;
                //5. ���� ��ü�� trcatched�� �־�д�
                trCatchedL = hit.transform;
                trCatchedL.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true, trCatchedL);
            }
            #endregion
        }

        if (trCatchedR && trCatchedR.gameObject.tag == "DisappearItem")
        {            
            switch (trCatchedR.gameObject.name)
            {
                case "Helmet":
                    weldingSceneMngr.instance.isHelmet = true;
                    Destroy(trCatchedR.gameObject);
                    return;
                case "WelderMask":
                    weldingSceneMngr.instance.isMask = true;
                    Destroy(trCatchedR.gameObject);
                    return;
                case "Papers1":
                case "Papers2":
                case "ToiletPaper":
                    weldingSceneMngr.instance.DeleteObj(trCatchedR.gameObject);
                    Destroy(trCatchedR.gameObject);
                    return;
            }
            trCatchedR = null;
        }
    }

    void DropObj()
    {
        //1. ���࿡ ������ �׷���ư�� ������
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (trCatchedR != null)
            {
                Throwobj(trCatchedR, OVRInput.Controller.RTouch);
                //2. ���� ��ü�� ���´�.(���� ��ü�� �θ� ���ش�)
                trCatchedR.parent = null;
                //Item������Ʈ�� isGrab�� false�� ������ش�
                trCatchedR.GetComponent<KHJ_Item>().isGrab = false;
                //3. trCatced�� ���� null�� �Ѵ�
                trCatchedR = null;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            if (trCatchedL != null)
            {
                Throwobj(trCatchedL, OVRInput.Controller.LTouch);
                //2. ���� ��ü�� ���´�.(���� ��ü�� �θ� ���ش�)
                trCatchedL.parent = null;
                //Item������Ʈ�� isGrab�� false�� ������ش�
                trCatchedL.GetComponent<KHJ_Item>().isGrab = false;
                //3. trCatced�� ���� null�� �Ѵ�
                trCatchedL = null;
            }
        }
    }

    void Throwobj(Transform obj, OVRInput.Controller controller)
    {
        SetKinematic(false, obj);
        //������ ���� (�̵��ӷ�)
        Vector3 dir = OVRInput.GetLocalControllerVelocity(controller);
        // ������ ȸ������ (ȸ���ӷ�)
        Vector3 angularDir = OVRInput.GetLocalControllerAngularVelocity(controller);
        //���� ��ü�� �پ��ִ� RIgidbody�� ��������
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        //Velocity ���� dir�� ����
        rb.velocity = dir * throwPower;
        // angularVelocity�� angulardir�� ����
        rb.angularVelocity = angularDir;
    }

    void SetKinematic(bool enable, Transform catchObj)
    {
        // ���� ��ü���� Rigidbody ������Ʈ �����´�
        Rigidbody rb = catchObj.GetComponent<Rigidbody>();
        // ������ ������Ʈ�� isKinematic�� false�Ѵ�
        rb.isKinematic = enable;
        //true �ϸ� ����X false�ϸ� ����O

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(trRight.position, 0.1f);
        Gizmos.DrawSphere(trLeft.position, 0.1f);
    }

}
