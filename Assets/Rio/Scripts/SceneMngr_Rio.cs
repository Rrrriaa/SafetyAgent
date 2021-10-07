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

    //0 비명 1 바람 2 쿵
    public void Scream()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[0]);//비명
        GetComponent<AudioSource>().PlayOneShot(clips[1]);//바람소리

    }

    public void StageFail()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[2]);//바람소리
        StartCoroutine(FadeInMono());
        StartCoroutine(FadeIn());
        EndCanvas.SetActive(true);
        EndText.text = "원인 : 추락사" + '\n' + "tip : 개구부에 덮개를 설치하세요";
        StartCoroutine(SceneReload(false));
    }

    //영상 컨트롤러
    public VideoPlayer videoplayer;
    public GameObject VideoCanvas;
    public void StageSuccess()
    {
        StartCoroutine(FadeIn());
        EndCanvas.SetActive(true);
        EndTitle.text = "스테이지 성공!";
        EndTitle.color = Color.blue;
        EndText.text = "작업을 안전하게 마무리했습니다." + '\n' + "수고하셨습니다!";
        //영상 재생 및 씬다시 리로드
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
        //성공 -> 첫화면으로 이동
        if (isSuccess)
        {
            //결과창 끄고
            //PipeProgressImg.gameObject.SetActive(false);
            EndCanvas.SetActive(false);
            //비디오 재생
            VideoCanvas.SetActive(true);
            videoplayer.Play();
            yield return new WaitForSeconds((float)videoplayer.clip.length + 1);
            SceneManager.LoadScene("Scene_00_Start");


        }
        //실패 
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
