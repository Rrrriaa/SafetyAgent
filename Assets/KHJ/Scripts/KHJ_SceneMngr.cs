using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public enum FAIL_INDEX
{
    HELMET,
    FIRE
}
public class KHJ_SceneMngr : MonoBehaviour
{

    public GameObject EndCanvas;
    public Text EndResult;
    public Text EndText;
    public AudioSource Audio;
    public AudioClip Success;
    public AudioClip Fail;

    ColorGrading color;
    // Start is called before the first frame update
    void Start()
    {
        var PostProcess = FindObjectOfType<PostProcessVolume>();
        color = PostProcess.profile.GetSetting<ColorGrading>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StageFail(FAIL_INDEX index)
    {
        if (!Audio.clip)
        {
            Audio.clip = Fail;
            Audio.Play();
        }
        StartCoroutine(FadeInMono());
        StartCoroutine(FadeIn(4f));
        EndCanvas.SetActive(true);
        EndResult.text = "�������� ����";
        //���ٽ� ���ε�
        StartCoroutine(SceneReload(false));
        switch (index)
        {
            case FAIL_INDEX.HELMET:
                EndText.text = "���� : ��ȣ��� ������" + '\n' + "tip : ��ȣ��� �����ϼ���";
                return;
            case FAIL_INDEX.FIRE:
                EndText.text = "���� : ��ȭ" + '\n' + "tip : ������ ������ �����ϼ���";
                return;
        }
    }
    //���� ��Ʈ�ѷ�
    public VideoPlayer videoplayer;
    public GameObject VideoCanvas;
    public void StageSuccess()
    {
        if (!Audio.clip)
        {
            Audio.clip = Success;
            Audio.Play();
        }
        StartCoroutine(FadeIn(140f));
        EndCanvas.SetActive(true);
        EndResult.text = "�������� ����";
        EndText.text = "�����ϰ� �۾��� �Ϸ��ϼ̽��ϴ�! �����ϼ̽��ϴ�!";        

        //���� ��� �� ���ٽ� ���ε�
        StartCoroutine(SceneReload(true));
    }


    float time = 0f;
    float F_time = 1f;
    float time1 = 0f;
    float F_time1 = 10f;
    protected IEnumerator FadeIn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ColorParameter a = color.colorFilter;
        Color alpha = a.value;
        time = 0f;
        while (alpha.r > 0f)
        {
            color.saturation.value = -100;
            time1 += Time.deltaTime / F_time1;
            alpha.r = Mathf.Lerp(1, 0, time1);
            alpha.g = Mathf.Lerp(1, 0, time1);
            alpha.b = Mathf.Lerp(1, 0, time1);
            color.colorFilter.value = alpha;
            yield return null;
        }
    }


    IEnumerator FadeInMono()
    {
        yield return new WaitForSeconds(1.5f);
        time = 0f;
        float alpha = color.saturation.value;
        while (alpha >= -100f)
        {
            time += Time.deltaTime / F_time;
            alpha = Mathf.Lerp(0, -100, time);
            color.saturation.value = alpha;
            yield return null;
        }
    }


    //��� �ؽ�Ʈâ
    public GameObject resultImg;
    protected IEnumerator SceneReload(bool isSuccess)
    {
        yield return new WaitForSecondsRealtime(3f);
        //���� -> ùȭ������ �̵�
        if (isSuccess)
        {
            //���â ����
            resultImg.SetActive(false);
            //���� ���
            VideoCanvas.SetActive(true);
            videoplayer.Play();
            yield return new WaitForSeconds(140f);
            SceneManager.LoadScene(0);
        }
        //���� 
        else
        {
            yield return new WaitForSecondsRealtime(4f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
