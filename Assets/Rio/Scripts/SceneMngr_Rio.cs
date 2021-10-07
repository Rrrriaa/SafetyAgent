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
    public Text EndTitle;
    public Text EndText;


    public AudioClip[] clips;

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

    //0 ��� 1 �ٶ� 2 ��
    public void Scream()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[0]);//���
        GetComponent<AudioSource>().PlayOneShot(clips[1]);//�ٶ��Ҹ�

    }

    public void StageFail()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[2]);//�ٶ��Ҹ�
        StartCoroutine(FadeInMono());
        StartCoroutine(FadeIn());
        EndCanvas.SetActive(true);
        EndText.text = "���� : �߶���" + '\n' + "tip : �����ο� ������ ��ġ�ϼ���";
        StartCoroutine(SceneReload(false));
    }

    //���� ��Ʈ�ѷ�
    public VideoPlayer videoplayer;
    public GameObject VideoCanvas;
    public void StageSuccess()
    {
        StartCoroutine(FadeIn());
        EndCanvas.SetActive(true);
        EndTitle.text = "�������� ����!";
        EndTitle.color = Color.blue;
        EndText.text = "�۾��� �����ϰ� �������߽��ϴ�." + '\n' + "�����ϼ̽��ϴ�!";
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
        time1 = 0f;
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
        yield return new WaitForSecondsRealtime(5f);
        //���� -> ùȭ������ �̵�
        if (isSuccess)
        {
            //���â ����
            //PipeProgressImg.gameObject.SetActive(false);
            EndCanvas.SetActive(false);
            //���� ���
            VideoCanvas.SetActive(true);
            videoplayer.Play();
            yield return new WaitForSeconds((float)videoplayer.clip.length + 1);
            SceneManager.LoadScene("Scene_00_Start");


        }
        //���� 
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
