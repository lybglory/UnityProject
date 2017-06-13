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

public class StartScenesGUI : MonoBehaviour {
    public GUISkin ParkourGUISkin;
    public Texture2D texturePlay;
    public Texture2D textureCancle;
    public Texture2D textureMinVol;
    private bool IsPlay;
    private bool IsCancle;

    private float horizonPlayBtn;             //开始Btn水平位置
    private float verticalPlayBtn;            //开始Btn垂直位置
    private float horizonCancleBtn;           //退出Btn水平位置
    private float verticalCancleBtn;          //退出Btn垂直位置

    private float horizonMinVolBtn;           //静音Btn水平位置
    private float verticalMinVolBtn;          //静音Btn垂直位置
    private float horizonNormalVolBtn;        //正常音Btn水平位置
    private float verticalNormalVolBtn;       //正常音Btn垂直位置
    private float horizonMaxVolBtn;           //最大音Btn水平位置
    private float verticalMaxVolBtn;          //最大音Btn垂直位置

    // Use this for initialization
    void Start () {
        //调用方法
        horizonPlayBtn = GetBtnXPosion(texturePlay);
        verticalPlayBtn = GetBtnYPosion(texturePlay);
        horizonCancleBtn = GetBtnXPosion(textureCancle);
        verticalCancleBtn = GetBtnYPosion(textureCancle);
    }

    private void OnGUI()
    {   //开始Btn,GUI是从左上角为0点+100位置居下100
        GUI.skin = ParkourGUISkin;
        if (GUI.Button(new Rect(horizonPlayBtn, verticalPlayBtn+100, texturePlay.width, texturePlay.height), "", ParkourGUISkin.GetStyle("Btn_Play"))) {
            Debug.Log("点击开始游戏");
        }

        //退出btn，GUI是从左上角为0点+200位置居下200，不让它重叠
        if (GUI.Button(new Rect(horizonCancleBtn,verticalCancleBtn+200,textureCancle.width,textureCancle.height),"",ParkourGUISkin.GetStyle("Btn_Cancle"))) {
            Debug.Log("点击退出");
        }

    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 获取btn水平位置
    /// </summary>
    /// <param name="btnTexture">传入texture2D</param>
    /// <returns>返回btn水平位置</returns>
    private float GetBtnXPosion(Texture2D btnTexture) {
        float btnHorizon;
        //居中位置
        return btnHorizon =(Screen.width - btnTexture.width) / 2;
    }

    /// <summary>
    /// 获取btn垂直位置
    /// </summary>
    /// <param name="btnTexture">传入texture2D</param>
    /// <returns>返回垂直位置</returns>
    private float GetBtnYPosion(Texture2D btnTexture)
    {
        float btnVertical;
        return btnVertical = (Screen.height - btnTexture.height) / 2;
    }
}
