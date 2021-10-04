using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Video;

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
    public GameObject PipeS;
    public GameObject PipePos;
    public bool isPipe;
    public GameObject PipeCanvas;
    public Image PipeProgressImg;
    public Text PipeText;

    public bool isHelmet;
    public bool isMask;
    
    public bool isWelding;
    public Burn[] burnArray;

    ColorGrading color;

    public GameObject EndCanvas;
    public Text EndResult;
    public Text EndText;

    public AudioSource Audio;
    public AudioClip Success;
    public AudioClip Fail;
    public AudioClip SetBatterySound;

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
        //Test();
        SetBattery();
        SetPipe();
        //currTime += Time.deltaTime;
        if (isWelding)
        {
            weldingSceneMngr.instance.PipeText.fontSize = 30;
            currTime += Time.deltaTime;
            float leftTime = SuccessTime - currTime;
            if(leftTime < 0)
            {
                leftTime = 0;
                PipeCanvas.SetActive(false);
            }
            PipeProgressImg.fillAmount = currTime / SuccessTime;
            string str = string.Format("{0:0.0}", leftTime);
            weldingSceneMngr.instance.PipeText.text = str;
        }
        CheckFire();
    }

    void SetBattery()
    {
        if(Torch.GetComponent<KHJ_Item>().isGrab || Battery.GetComponent<KHJ_Item>().isGrab)
        {
            if (isBattery)
            {
                //�ε��� ��ü�� �������� �ڽ����� �Ѵ�
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
            for(int i = 0; i < burnArray.Length; i++)
            {
                if(burnArray[i])
                    burnArray[i].ActiveFireEft();
            }
        }
        if (currTime > SuccessTime)
        {
            StageSuccess();
        }
    }

    public void DeleteObj(GameObject obj)
    {
        print("wgfhkfhslkjgl : " + burnArray.Length);
        for (int i = 0; i < burnArray.Length; i++)
        {
            if (burnArray[i] == null)
                continue;
            if (burnArray[i].gameObject == obj)
            {
                burnArray[i] = null;
                break;
            }
        }
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
        if (index == FAIL_INDEX.HELMET)
            EndText.text = "���� : ��ȣ��� ������" + '\n' +"tip : ��ȣ��� �����ϼ���";
        if (index == FAIL_INDEX.FIRE)
            EndText.text = "���� : ��ȭ" + '\n' + "tip : ������ ������ �����ϼ���";
        //���ٽ� ���ε�
        StartCoroutine(SceneReload(false));
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
        PipeS.AddComponent<Rigidbody>();

       //���� ��� �� ���ٽ� ���ε�
        StartCoroutine(SceneReload(true));
    }


    float time = 0f;
    float F_time = 1f;
    float time1 = 0f;
    float F_time1 = 10f;
    IEnumerator FadeIn(float waitTime)
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
    IEnumerator SceneReload(bool isSuccess)
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
