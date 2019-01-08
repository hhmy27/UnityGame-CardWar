using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class GameManager : Singleton<GameManager>
{
    public bool isPaused = false;
    //public int Whichscene = 1;//存储现在在哪个场景 0是幻灯片场景 1是start场景 2是choice场景 3是游戏场景
    public enum Scene { BackGroundIntro, Start,ChioceMode,ChoiceHero,Rest,Test,Test1,Test2,Last};
    public enum GameState { Pause,Gameing };
    public Scene WhichScene=GameManager.Scene.BackGroundIntro;
    private bool HadChooseHero=false;//是否选择过英雄如果已经选择过了就不再显示choice场景
    private bool isFirstPlaygame = true;//如不是第一次玩就显示故事背景
    public int GameLevel = 1;//游戏关卡



    private void Awake()
    {
        //游戏开始时是暂停的状态
    }
    private void Update()
    {
        //if (WhichScene == Scene.ChioceMode)//游戏场景可暂停
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        //暂停UI显示在这里
        //        //Pause();
        //    }
        //}



    }
    public void Pause()
    {

        isPaused = true;
        Time.timeScale = 0;
        //Cursor.visible = true;

        //
    }
    //非暂停状态
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = 1;
        //Cursor.visible = false;
    }


    public void Setscene(Scene Num)
    {
        WhichScene = Num;
    }
    private Save CreateSaveGO()
    {
        Save save = new Save();

        //这里把当前游戏数据存入save里
        return save;
    }
    private void SetGame(Save save)
    {

        //据从json读取的 save设置单例类的数据以达到读取游戏数据的作用

    }
    //JSON:存档和读档
    private void SaveByJson()
    {
        Save save = CreateSaveGO();
        string filePath = Application.dataPath + "/StreamingFile" + "/byJson.json";
        //利用JsonMapper将save对象转换为Json格式的字符串
        string saveJsonStr = JsonMapper.ToJson(save);
        //将这个字符串写入到文件中
        //创建一个StreamWriter，并将字符串写入文件中
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        //关闭StreamWriter
        sw.Close();

        UIManager.Instance.ShowMessage("保存成功");
    }
    private void LoadByJson()
    {
        string filePath = Application.dataPath + "/StreamingFile" + "/byJson.json";
        if (File.Exists(filePath))
        {
            //创建一个StreamReader，用来读取流
            StreamReader sr = new StreamReader(filePath);
            //将读取到的流赋值给jsonStr
            string jsonStr = sr.ReadToEnd();
            //关闭
            sr.Close();

            //将字符串jsonStr转换为Save对象
            Save save = JsonMapper.ToObject<Save>(jsonStr);
            SetGame(save);
            UIManager.Instance.ShowMessage("");
        }
        else
        {
           // UIManager._instance.ShowMessage("存档文件不存在");
        }
    }
    //从暂停状态恢复到非暂停状态
    public void ContinueGame()
    {
        UnPause();
        UIManager.Instance.ShowMessage("");
    }
    //重新开始游戏
    public void NewGame()
    {
        //重置所有游戏数据开启新的游戏

    }
    //退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }

    //保存游戏
    public void SaveGame()
    {
        SaveByJson();  
    }
    //加载游戏
    public void LoadGame()
    {
        LoadByJson();  
    }
}