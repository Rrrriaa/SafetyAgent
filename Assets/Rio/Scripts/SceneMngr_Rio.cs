using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SceneMngr_Rio : MonoBehaviour
{
    public static SceneMngr_Rio instance;

    ColorGrading color;

    public GameObject EndCanvas;
    public Text EndText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
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

    public void StageFail()
    {
        StartCoroutine(FadeInMono());
        StartCoroutine(FadeIn());
        EndCanvas.SetActive(true);
        EndText.text = "���� : �߶���";
    }

    //���� ��Ʈ�ѷ�
    public VideoPlayer videoplayer;
    public GameObject VideoCanvas;
    public void StageSuccess()
    {
        StartCoroutine(FadeIn());
        //EndCanvas.SetActive(true);
        //EndResult.text = "�������� ����";
        //EndText.text = "�����ϰ� �۾��� �Ϸ��ϼ̽��ϴ�! �����ϼ̽��ϴ�!";

        //���� ��� �� ���ٽ� ���ε�
        StartCoroutine(SceneReload(true));
    }

    float time = 0f;
    float F_time = 1f;
    float time1 = 0f;
    float F_time1 = 10f;
    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(3f);
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

    IEnumerator SceneReload(bool isSuccess)
    {
        yield return new WaitForSecondsRealtime(3f);
        //���� -> ùȭ������ �̵�
        if (isSuccess)
        {
            //���â ����
            //PipeProgressImg.gameObject.SetActive(false);
            //���� ���
            VideoCanvas.SetActive(true);
            videoplayer.Play();
            yield return new WaitForSeconds(140f);
            SceneManager.LoadScene(0);


        }
        //���� 
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
