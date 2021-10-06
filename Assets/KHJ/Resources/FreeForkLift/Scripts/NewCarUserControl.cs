using System;
using UnityEngine;

    [RequireComponent(typeof (NewCarController))]
public class NewCarUserControl : MonoBehaviour
    {
        private NewCarController m_Car; // the car controller we want to use

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<NewCarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            //float h = Input.GetAxis("Horizontal");
            //float v = Input.GetAxis("Vertical");
             
            Vector2 stickPos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.LTouch);
            Vector2 stickPos1 = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, OVRInput.Controller.RTouch);
            
#if !MOBILE_INPUT
            float handbrake = Input.GetAxis("Jump");
            m_Car.Move(stickPos1.x, stickPos.y, stickPos.y, handbrake);
            
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }
    }

