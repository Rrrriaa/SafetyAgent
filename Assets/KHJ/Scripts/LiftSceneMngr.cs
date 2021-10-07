using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSceneMngr : KHJ_SceneMngr
{
    public static LiftSceneMngr instance;
    //적재할 박스 포지션
    public GameObject[] Boxes;
    public int StackedBoxNum;

    public GameObject Player1;
    public GameObject Player2;
    public NewCarUserControl userControl;
    public bool isRide;
    public GameObject guideLine;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StackBox();
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            StageFail(FAIL_INDEX.FALLBOX);
        }
        CheckRide();
    }

    public void StackBox()
    {
        for (int i = 0; i < Boxes.Length; i++)
        {
            if (!Boxes[i].activeSelf)
            {
                Boxes[i].SetActive(true);
                StackedBoxNum++;
                return;
            }
        }
    }

    void CheckRide()
    {
        if (isRide)
        {
            Player1.transform.parent = Player2.transform;
            Player1.transform.position = Player2.transform.position;
            Player1.GetComponent<CharacterController>().enabled = false;
            Player1.GetComponent<PlayerMove_Rio>().enabled = false;
            guideLine.SetActive(true);
        }
        else
        {
            Player1.GetComponent<CharacterController>().enabled = true;
            Player1.transform.parent = null;
        }
        userControl.enabled = isRide;
    }

}
