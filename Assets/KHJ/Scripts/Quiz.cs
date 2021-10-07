using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Question
{
    public string context;
    public Text Q;
    public GameObject Correct;
    public GameObject Wrong;
    public bool isCorrect;
}

public class Quiz : MonoBehaviour
{
    public Question question;


    void Start()
    {
        question.Q.text = question.context;
    }

    void Update()
    {
        
    }

    public void CheckAnswer(bool answer)
    {
        print("버튼 입력");
        if (answer)
            Correct();
        else
            Wrong();
        QuizMngr.instance.PerfectCheck();
    }

    void Correct()
    {        
        question.isCorrect = true;
        question.Wrong.SetActive(false);
        question.Correct.SetActive(true);
    }
    void Wrong()
    {
        question.isCorrect = false;
        question.Correct.SetActive(false);
        question.Wrong.SetActive(true);
    }


}
