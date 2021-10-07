using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizMngr : MonoBehaviour
{
    public static QuizMngr instance;
    public List<Quiz> quizzes;
    public bool isPerfect;
    public GameObject PerfectImg;
    public int correctNum;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (isPerfect)
        {
            PerfectImg.SetActive(true);            
        }
        else
        {
            PerfectImg.SetActive(false);
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Scene_00_Start");
    }


    public void PerfectCheck()
    {
        correctNum = 0;
        for (int i = 0; i < quizzes.Count; i++)
        {
            if (quizzes[i].question.isCorrect)
            {
                correctNum++;
            }            
        }
        if(correctNum == quizzes.Count)
        {
            isPerfect = true;
            StartCoroutine(nextScene());
        }
        else
        {
            isPerfect = false;
        }
    }

}
