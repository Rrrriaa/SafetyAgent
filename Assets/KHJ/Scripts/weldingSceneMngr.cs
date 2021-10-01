using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public enum FAIL_INDEX
{
    HELMET,
    FIRE
}

public class weldingSceneMngr : MonoBehaviour
{
    public static weldingSceneMngr instance;

    public GameObject Torch;
    public GameObject Battery;
    public bool isBattery;
    public GameObject Pipe;
    public GameObject PipePos;
    public bool isPipe;

    public bool isHelmet;
    public bool isMask;
    
    public bool isWelding;

    ColorGrading color;

    public GameObject EndCanvas;
    public Text EndText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        var PostProcess = FindObjectOfType<PostProcessVolume>();
        color = PostProcess.profile.GetSetting<ColorGrading>();
    }


    public float currTime = 0;
    float FireTime = 2;
    float SuccessTime = 10;
    void Update()
    {
        Test();
        SetBattery();
        SetPipe();

        if (isWelding)
        {
            currTime += Time.deltaTime;
        }
        CheckFire();
    }

    void SetBattery()
    {
        if(Torch.GetComponent<KHJ_Item>().isGrab || Battery.GetComponent<KHJ_Item>().isGrab)
        {
            if (isBattery)
            {
                //부딪힌 물체를 오른손의 자식으로 한다
                Battery.transform.parent = Torch.transform;
                Battery.transform.position = Torch.transform.position;
                Battery.transform.rotation = Torch.transform.rotation;
                Battery.layer = 0;
                Battery.GetComponent<Collider>().enabled = false;
                Battery.GetComponent<Rigidbody>().isKinematic = true;
                Torch.GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    void SetPipe()
    {
        if (isPipe)
        {
            Pipe.transform.position = PipePos.transform.position;
            Pipe.transform.rotation = PipePos.transform.rotation;

            Pipe.GetComponent<Rigidbody>().isKinematic = true;
            Pipe.layer = 0;
        }
    }
    void Test()
    {
        isBattery = true;
        Battery.transform.parent = Torch.transform;
        Battery.transform.position = Torch.transform.position;
        Battery.transform.rotation = Torch.transform.rotation;
        Battery.layer = 0;
        Battery.GetComponent<Collider>().enabled = false;
        Battery.GetComponent<Rigidbody>().isKinematic = true;
        Torch.GetComponent<Rigidbody>().isKinematic = true;
    }
    void CheckFire()
    {
        if (currTime > FireTime)
        {

        }
        if (currTime > SuccessTime)
        {
            StageSuccess();
        }
    }

    public void StageFail(FAIL_INDEX index)
    {
        StartCoroutine(FadeInMono());
        EndCanvas.SetActive(true);

        if (index == FAIL_INDEX.HELMET)
            EndText.text = "보호장비 미착용으로 인한 부상";
        if (index == FAIL_INDEX.FIRE)
            EndText.text = "발화";
    }
    public void StageSuccess()
    {

    }


    float time = 0f;
    float F_time = 1f;
    IEnumerator FadeIn(Image obj)
    {
        obj.gameObject.SetActive(true);
        time = 0f;
        Color alpha = obj.color;
        while (alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            alpha.a = Mathf.Lerp(0, 1, time);
            obj.color = alpha;
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
}
