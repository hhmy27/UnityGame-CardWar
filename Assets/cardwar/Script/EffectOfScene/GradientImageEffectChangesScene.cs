using UnityEngine;
using System.Collections;

//淡入淡出跳转场景  
public class GradientImageEffectChangesScene : MonoBehaviour
{
    //载入图的绘制深度  
    public int guiDepth = 0;
    //要加载的场景名  
    public string levelToLoad = "";
    //载入界面图片  
    public Texture2D splashLogo;
    //淡入淡出速度  
    public float fadeSpeed = 1.0f;
    //等待时间  
    public float waitTime = 0.2f;
    //是否等待任意操作跳转  
    public bool waitForInput = false;
    //是否自动跳转  
    public bool startAutomatically = true;
    //淡出的停留时间  
    private float timeFadingInFinished = 0.0f;
    //淡出方式(分为先加载后淡出和先淡出后加载两种)  
    public enum SplashType
    {
        LoadNextLevelThenFadeOut,
        FadeOutThenLoadNextLevel
    }
    public SplashType splashType;
    //透明度  
    private float alpha = 0.0f;
    //淡入淡出的状态  
    private enum FadeStatus
    {
        Paused,
        FadeIn,
        FadeWaiting,
        FadeOut
    }
    private FadeStatus status = FadeStatus.FadeIn;
    //摄像机  
    private Camera oldCam;
    private GameObject oldCamGO;
    //载入图绘制范围  
    private Rect splashLogoPos = new Rect();
    //载入图位置  
    public enum LogoPositioning
    {
        Centered,
        Stretched
    }
    public LogoPositioning logoPositioning;
    //是否绘制下个场景  
    private bool loadingNextLevel = false;

    void Start()
    {
        //是否自动淡入淡出  
        if (startAutomatically)
        {
            status = FadeStatus.FadeIn;
        }
        else
        {
            status = FadeStatus.Paused;
        }
        //指定摄像机  
        oldCam = Camera.main;
        oldCamGO = Camera.main.gameObject;
        //载入图位置大小判断  
        if (logoPositioning == LogoPositioning.Centered)
        {
            splashLogoPos.x = (Screen.width * 0.5f) - (splashLogo.width * 0.5f);
            splashLogoPos.y = (Screen.height * 0.5f) - (splashLogo.height * 0.5f);

            splashLogoPos.width = splashLogo.width;
            splashLogoPos.height = splashLogo.height;
        }
        else
        {
            splashLogoPos.x = 0;
            splashLogoPos.y = 0;

            splashLogoPos.width = Screen.width;
            splashLogoPos.height = Screen.height;
        }

        if (splashType == SplashType.LoadNextLevelThenFadeOut)
        {
            // DontDestroyOnLoad(this);
            // DontDestroyOnLoad(Camera.main);
        }
        //判断待加载场景是否为空  
        if ((Application.levelCount <= 1) || (levelToLoad == ""))
        {
            Debug.LogWarning("Invalid levelToLoad value.");
        }
    }

    //外部调用接口执行淡入淡出转场景  
    public void StartSplash()
    {
        status = FadeStatus.FadeIn;
    }

    void Update()
    {
        //状态机判断  
        switch (status)
        {
            case FadeStatus.FadeIn:
                alpha += fadeSpeed * Time.deltaTime;
                break;
            case FadeStatus.FadeWaiting:
                if ((!waitForInput && Time.time >= timeFadingInFinished + waitTime) || (waitForInput && Input.anyKey))
                {
                    status = FadeStatus.FadeOut;
                }
                break;
            case FadeStatus.FadeOut:
                alpha += -fadeSpeed * Time.deltaTime;
                break;
        }
    }

    void OnGUI()
    {
        //图片Alpha控制  
        GUI.depth = guiDepth;
        if (splashLogo != null)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, Mathf.Clamp01(alpha));
            GUI.DrawTexture(splashLogoPos, splashLogo);
        }
        if (alpha > 1.0f)
        {
            status = FadeStatus.FadeWaiting;
            timeFadingInFinished = Time.time;
            alpha = 1.0f;
            if (splashType == SplashType.LoadNextLevelThenFadeOut)
            {
                oldCam.depth = -1000;
                loadingNextLevel = true;
                if ((Application.levelCount) >= 1 && (levelToLoad != ""))
                {
                    //Application.LoadLevel(levelToLoad);
                }
            }
        }
        if (alpha < 0.0f)
        {
            if (splashType == SplashType.FadeOutThenLoadNextLevel)
            {
                if ((Application.levelCount >= 1) || (levelToLoad != ""))
                {
                    //Application.LoadLevel(levelToLoad);
                }
            }
            else
            {
                //Destroy(oldCamGO);
                // Destroy(oldCam);
            }
        }
    }

    //场景加载完毕后销毁摄像机和摄像机物体  
    void OnLevelWasLoaded(int lvlIdx)
    {
        if (loadingNextLevel)
        {
            if (alpha == 0.0f)
            {
                // Destroy(oldCam);
                //Destroy(oldCamGO);
            }
        }
    }

    //绘制Gizmos  
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}