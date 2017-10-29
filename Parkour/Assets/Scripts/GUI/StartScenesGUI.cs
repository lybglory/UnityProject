/***
*	title:GUI
*	[副标题]
*	Description：GUI界面的交互
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScenesGUI : MonoBehaviour {
    public GUISkin ParkourGUISkin;
    public Texture2D texturePlay;
    public Texture2D textureCancle;
    public Texture2D textureVol;
    private bool IsPlay;
    private bool IsCancle;

    private float horizonPlayBtn;             //开始Btn水平位置
    private float verticalPlayBtn;            //开始Btn垂直位置
    private float horizonCancleBtn;           //退出Btn水平位置
    private float verticalCancleBtn;          //退出Btn垂直位置

    private float horizonVolBtn;           //音量Btn水平位置
    private float verticalVolBtn;          //音量Btn垂直位置

    private string strVolBtn = "Btn_MaxVol";//音量btn名称

    private AudioSource audioStartBg;            //音量组件

    private void Awake()
    {   //持有音频组件
        audioStartBg = GameObject.Find("_AudioManager/Audio").GetComponent<AudioSource>();
        audioStartBg.Play();
        audioStartBg.loop = true;
    }

    void Start() {
        //调用方法
        horizonPlayBtn = GetBtnXPosion(texturePlay);
        verticalPlayBtn = GetBtnYPosion(texturePlay);
        horizonCancleBtn = GetBtnXPosion(textureCancle);
        verticalCancleBtn = GetBtnYPosion(textureCancle);

        horizonVolBtn = GetBtnXPosion(textureVol);
        verticalVolBtn = GetBtnYPosion(textureVol);

        GlobalManager.GlVol = EnumVolume.MaxVolu;//默认全音量
    }

    private void OnGUI()
    {   //开始Btn,GUI是从左上角为0点+100位置居下100
        GUI.skin = ParkourGUISkin;
        if (GUI.Button(new Rect(horizonPlayBtn, verticalPlayBtn, texturePlay.width, texturePlay.height), "", ParkourGUISkin.GetStyle("Btn_Play"))) {
            Debug.Log("点击开始游戏");
            SceneManager.LoadSceneAsync("2_LevelOne");
        }

        //退出btn，GUI是从左上角为0点+200位置居下200，不让它重叠
        if (GUI.Button(new Rect(horizonCancleBtn, verticalCancleBtn + 80, textureCancle.width, textureCancle.height), "", ParkourGUISkin.GetStyle("Btn_Cancle"))) {
            Debug.Log("点击退出");
        }

        if (GUI.Button(new Rect(horizonVolBtn, verticalVolBtn, textureVol.width, textureVol.height), "", ParkourGUISkin.GetStyle(strVolBtn)))
        {
            Changevolume(GlobalManager.GlVol);
        }
        GUI.Label(new Rect(Screen.width /2, 0, 500, 500), "疯狂跑酷",ParkourGUISkin.GetStyle("Label_Title"));

    }

    // Update is called once per frame
    void Update() {

    }

    /// <summary>
    /// 获取btn水平位置
    /// </summary>
    /// <param name="btnTexture">传入texture2D</param>
    /// <returns>返回btn水平位置</returns>
    private float GetBtnXPosion(Texture2D btnTexture) {
        float btnHorizon;
        //音量按钮就不是居中了。不走此句代码
        if (btnTexture != textureVol) {
            btnHorizon = (Screen.width - btnTexture.width) / 2;
        }
        else {//音量按钮处理
            btnHorizon = Screen.width - btnTexture.width;
        }
        //居中位置
        return btnHorizon;
    }

    /// <summary>
    /// 获取btn垂直位置
    /// </summary>
    /// <param name="btnTexture">传入texture2D</param>
    /// <returns>返回垂直位置</returns>
    private float GetBtnYPosion(Texture2D btnTexture)
    {
        float btnVertical;
        //当不是音量按钮的时候
        if (btnTexture != textureVol)
        {

            btnVertical = (Screen.height - btnTexture.height) / 2;
        }
        else {
            btnVertical = Screen.height - btnTexture.height;
        }
        return btnVertical;
    }

    public void Changevolume(EnumVolume VolumeBtn){
        switch (VolumeBtn) {
            //存储登入场景中设置的音量枚举，其他关卡场景根据这个枚举设置背景音乐的大小
            case EnumVolume.None:
                audioStartBg.volume = 0;                            //设置最小音量
                break;
            case EnumVolume.MaxVolu:
                GlobalManager.GlVol = EnumVolume.MinVolu;  
                strVolBtn = "Btn_MinVol";                       //设置静音音量的btn，赋值给全局变量。
                audioStartBg.volume = 0;                            //设置最小音量
                break;
            case EnumVolume.MinVolu:
                GlobalManager.GlVol = EnumVolume.NormalVolu;
                strVolBtn = "Btn_NormalVol";                    //设置最小音量的btn，赋值给全局变量。
                audioStartBg.volume = 0.5f;                          //设置正常音量
                break;
            case EnumVolume.NormalVolu:
                GlobalManager.GlVol = EnumVolume.MaxVolu;       //存储音量枚举变量，便于其他场景传值。
                strVolBtn = "Btn_MaxVol";                       //设置最大音量的btn，赋值给全局变量。
                audioStartBg.volume = 1f;                            //设置最大音量
                break;
        }
    }
    
}
