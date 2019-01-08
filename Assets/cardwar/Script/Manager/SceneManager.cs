using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor.SceneManagement;

public class SceneManager : Singleton<SceneManager> {


    /// <summary>
    /// 转场时调用函数
    /// </summary>
    /// <param 场景编号="Num"></param>
    /// <param 场景名="SceneName"></param>
    public void ChangeScene(GameManager.Scene Num ,string SceneName)
    {
        
        GameManager.Instance.Setscene(Num);
        //EditorSceneManager.LoadScene(SceneName);
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)Num);

        //Application.loadedLevel
        Invoke("DelayIninMessagBox", 1.5f);
    }

    private void DelayIninMessagBox()
    {
        ControlMessageBox.Instance.InitMessageBox();
    }
}
