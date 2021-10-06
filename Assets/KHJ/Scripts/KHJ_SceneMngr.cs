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
        EndResult.text = "스테이지 실패";
        //씬다시 리로드
        StartCoroutine(SceneReload(false));
        switch (index)
        {
            case FAIL_INDEX.HELMET:
                EndText.text = "원인 : 보호장비 미착용" + '\n' + "tip : 보호장비를 착용하세요";
                return;
            case FAIL_INDEX.FIRE:
                EndText.text = "원인 : 발화" + '\n' + "tip : 가연성 물질을 제거하세요";
                return;
        }
    }
    //영상 컨트롤러
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
        EndResult.text = "스테이지 성공";
        EndText.text = "안전하게 작업을 완료하셨습니다! 수고하셨습니다!";        

        //영상 재생 및 씬다시 리로드
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


    //결과 텍스트창
    public GameObject resultImg;
    protected IEnumerator SceneReload(bool isSuccess)
    {
        yield return new WaitForSecondsRealtime(3f);
        //성공 -> 첫화면으로 이동
        if (isSuccess)
        {
            //결과창 끄고
            resultImg.SetActive(false);
            //비디오 재생
            VideoCanvas.SetActive(true);
            videoplayer.Play();
            yield return new WaitForSeconds(140f);
            SceneManager.LoadScene(0);
        }
        //실패 
        else
        {
            yield return new WaitForSecondsRealtime(4f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
