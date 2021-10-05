using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KHJ_HandCtrl : MonoBehaviour
{

    //오른손 Transform
    public Transform trRight;
    //왼손 Transform
    public Transform trLeft;

    //잡은 물체의 Transform
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
        //1. 만약에 오른쪽 컨트롤러의 그랩버튼을 누르면
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            //2. 오른손위치, 오른손앞방향에서 나가는 Ray를 만든다
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
                //4. 부딪힌 물체를 잡는다. (부딪힌 물체를 오른손의 자식으로 한다)
                hit.transform.parent = trRight;
                hit.transform.position = trRight.position;
                hit.transform.rotation = trRight.rotation;
                //5. 잡은 물체를 trcatched에 넣어둔다
                trCatchedR = hit.transform;
                trCatchedR.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true, trCatchedR);
            }
            #endregion
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            //2. 왼손위치, 왼손앞방향에서 나가는 Ray를 만든다
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCast
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.1f, out hit, 1, layer))
            {
                //4. 부딪힌 물체를 잡는다. (부딪힌 물체를 왼손의 자식으로 한다)
                hit.transform.parent = trLeft;
                hit.transform.position = trLeft.position;
                hit.transform.rotation = trLeft.rotation;
                //5. 잡은 물체를 trcatched에 넣어둔다
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
        //1. 만약에 오른쪽 그랩버튼을 놓으면
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            if (trCatchedR != null)
            {
                Throwobj(trCatchedR, OVRInput.Controller.RTouch);
                //2. 잡은 물체를 놓는다.(잡은 물체의 부모를 없앤다)
                trCatchedR.parent = null;
                //Item컴포넌트의 isGrab를 false로 만들어준다
                trCatchedR.GetComponent<KHJ_Item>().isGrab = false;
                //3. trCatced의 값을 null로 한다
                trCatchedR = null;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            if (trCatchedL != null)
            {
                Throwobj(trCatchedL, OVRInput.Controller.LTouch);
                //2. 잡은 물체를 놓는다.(잡은 물체의 부모를 없앤다)
                trCatchedL.parent = null;
                //Item컴포넌트의 isGrab를 false로 만들어준다
                trCatchedL.GetComponent<KHJ_Item>().isGrab = false;
                //3. trCatced의 값을 null로 한다
                trCatchedL = null;
            }
        }
    }

    void Throwobj(Transform obj, OVRInput.Controller controller)
    {
        SetKinematic(false, obj);
        //던지는 방향 (이동속력)
        Vector3 dir = OVRInput.GetLocalControllerVelocity(controller);
        // 던지는 회전방향 (회전속력)
        Vector3 angularDir = OVRInput.GetLocalControllerAngularVelocity(controller);
        //잡은 물체에 붙어있는 RIgidbody를 가져오자
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        //Velocity 값에 dir을 넣자
        rb.velocity = dir * throwPower;
        // angularVelocity에 angulardir를 넣자
        rb.angularVelocity = angularDir;
    }

    void SetKinematic(bool enable, Transform catchObj)
    {
        // 잡은 물체에서 Rigidbody 컴포넌트 가져온다
        Rigidbody rb = catchObj.GetComponent<Rigidbody>();
        // 가져온 컴포넌트의 isKinematic을 false한다
        rb.isKinematic = enable;
        //true 하면 물리X false하면 물리O

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(trRight.position, 0.1f);
        Gizmos.DrawSphere(trLeft.position, 0.1f);
    }

}
