using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMan : MonoBehaviour
{
    public string[] serihu;
    public Text dialogueText;
    public GameObject nextButton;
    public GameObject preButton;
    public int nowIndex;

    void Start()
    {
        StartSetting();
    }

    void Update()
    {
        
    }

    void StartSetting()
    {
        nowIndex = -1;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (nowIndex == serihu.Length - 1)
            return;
        nowIndex++;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(serihu[nowIndex]));     //UI에 나타내기
    }
    public void DisplayPreSentence()
    {
        if (nowIndex == 0)
            return;
        nowIndex--;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(serihu[nowIndex]));     //UI에 나타내기
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;            //한글자씩 화면에 반영
            yield return null;
        }
    }




}
