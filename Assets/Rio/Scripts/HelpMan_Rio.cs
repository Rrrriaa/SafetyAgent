using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMan_Rio : MonoBehaviour
{
    public string[] serihu;
    public Text dialogueText;
    //public GameObject nextButton;
    //public GameObject preButton;
    public int nowIndex;

    //�÷��̾� ȿ����
    public List<AudioClip> EFT_clips;
    AudioSource player;


    void Start()
    {
        player = GetComponent<AudioSource>();
        StartSetting();

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
        EFT_Sound(); 
        PlayAnim(nowIndex);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(serihu[nowIndex]));     //UI�� ��Ÿ����
    }
    public void DisplayPreSentence()
    {
        if (nowIndex == 0)
            return;
        nowIndex--;
        EFT_Sound();
        PlayAnim(nowIndex);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(serihu[nowIndex]));     //UI�� ��Ÿ����
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (letter == '.')
            {
                dialogueText.text += '\n';
                continue;
            }
            dialogueText.text += letter;            //�ѱ��ھ� ȭ�鿡 �ݿ�
            yield return null;
        }
    }

    void EFT_Sound()
    {
        int idx = Random.Range(0, EFT_clips.Count);
        player.clip = EFT_clips[idx];
        player.Play();
    }

    
    void PlayAnim(int idx)
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("Anim"+idx);
        print("Anim" + idx);
    }

}
