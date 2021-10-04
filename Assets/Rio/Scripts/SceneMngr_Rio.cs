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
        EndText.text = "원인 : 추락사";
    }

    //영상 컨트롤러
    public VideoPlayer videoplayer;
    public GameObject VideoCanvas;
    public void StageSuccess()
    {
        StartCoroutine(FadeIn());
        //EndCanvas.SetActive(true);
        //EndResult.text = "스테이지 성공";
        //EndText.text = "안전하게 작업을 완료하셨습니다! 수고하셨습니다!";

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
        //성공 -> 첫화면으로 이동
        if (isSuccess)
        {
            //결과창 끄고
            //PipeProgressImg.gameObject.SetActive(false);
            //비디오 재생
            VideoCanvas.SetActive(true);
            videoplayer.Play();
            yield return new WaitForSeconds(140f);
            SceneManager.LoadScene(0);


        }
        //실패 
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }


    }
}
