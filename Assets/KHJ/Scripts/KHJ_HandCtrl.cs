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
        //1. 만약에 오른쪽 컨트롤러의 그랩버튼을 누르면
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            //2. 오른손위치, 오른손앞방향에서 나가는 Ray를 만든다
            Ray ray = new Ray(trRight.position, trRight.forward);
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCastAll
            RaycastHit[] hits = Physics.SphereCastAll(ray, 0.5f, 1, layer);
            //만약에 부딫힌게 있다면
            if (hits.Length > 0)
            {
                //4.부딪힌 물체중 가장 가까운 물체를 잡는다. (부딪힌 물체를 오른손의 자식으로 한다)
                hits[0].transform.parent = trRight;
                //5. 잡은 물체를 trCatched에 넣어둔다
                trCatchedR = hits[0].transform;
                //Item컴포넌트의 isGrab를 true로 만들어준다
                trCatchedR.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true);
            }
            #endregion
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            //2. 왼손위치, 왼손앞방향에서 나가는 Ray를 만든다
            Ray ray = new Ray(trLeft.position, trLeft.forward);
            int layer = 1 << LayerMask.NameToLayer("CatchObj");

            #region SpereCastAll
            RaycastHit[] hits = Physics.SphereCastAll(ray, 0.5f, 1, layer);
            //만약에 부딫힌게 있다면
            if (hits.Length > 0)
            {
                //4.부딪힌 물체중 가장 가까운 물체를 잡는다. (부딪힌 물체를 오른손의 자식으로 한다)
                hits[0].transform.parent = trLeft;
                //5. 잡은 물체를 trCatched에 넣어둔다
                trCatchedL = hits[0].transform;
                //Item컴포넌트의 isGrab를 true로 만들어준다
                trCatchedL.GetComponent<KHJ_Item>().isGrab = true;
                SetKinematic(true);
            }
            #endregion
        }

    }

    void DropObj()
    {
        //1. 만약에 오른쪽 그랩버튼을 놓으면
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            if (trCatchedR != null)
            {
                Throwobj();
                //2. 잡은 물체를 놓는다.(잡은 물체의 부모를 없앤다)
                trCatchedR.parent = null;
                //3. trCatced의 값을 null로 한다
                trCatchedR = null;
                //Item컴포넌트의 isGrab를 false로 만들어준다
                trCatchedR.GetComponent<KHJ_Item>().isGrab = false;
            }
        }
        if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            if (trCatchedL != null)
            {
                Throwobj();
                //2. 잡은 물체를 놓는다.(잡은 물체의 부모를 없앤다)
                trCatchedL.parent = null;
                //3. trCatced의 값을 null로 한다
                trCatchedL = null;
                //Item컴포넌트의 isGrab를 false로 만들어준다
                trCatchedL.GetComponent<KHJ_Item>().isGrab = false;
            }
        }
    }

    void Throwobj()
    {
        SetKinematic(false);
        //던지는 방향 (이동속력)
        Vector3 dir = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        // 던지는 회전방향 (회전속력)
        Vector3 angularDir = OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch);
        //잡은 물체에 붙어있는 RIgidbody를 가져오자
        Rigidbody rb = trCatchedR.GetComponent<Rigidbody>();
        //Velocity 값에 dir을 넣자
        rb.velocity = dir * throwPower;
        // angularVelocity에 angulardir를 넣자
        rb.angularVelocity = angularDir;
    }

    void SetKinematic(bool enable)
    {
        // 잡은 물체에서 Rigidbody 컴포넌트 가져온다
        Rigidbody rb = trCatchedR.GetComponent<Rigidbody>();
        // 가져온 컴포넌트의 isKinematic을 false한다
        rb.isKinematic = enable;
        //true 하면 물리X false하면 물리O

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(trRight.position, 1);
        Gizmos.DrawSphere(trLeft.position, 1);
    }
}
