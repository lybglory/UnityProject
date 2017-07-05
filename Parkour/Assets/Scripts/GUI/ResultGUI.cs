/***
*	title:"跑酷"项目开发
*	[副标题]
*	Description：实现计分功能
*	            绘制背景贴图
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGUI : MonoBehaviour {
    public Texture2D TextureRedDiamond;
    public Texture2D TextureBlueDiamond;
    public Texture2D TextureResultBg;
    public GUISkin ResultGUISkin;
    private float XcenterPosition;
    private float YcenterPosition;

    private void OnGUI()
    {   //需要给GUI的皮肤赋值。字体的大小才会生效
        GUI.skin = ResultGUISkin;
        //绘制背景贴图,new Rect（屏幕中间的X轴position，Y轴position，贴图的长度，贴图的宽度）
        GUI.DrawTexture(new Rect(XcenterPosition, YcenterPosition, TextureResultBg.width,TextureResultBg.height), TextureResultBg);
        //绘制路程
        GUI.Label(new Rect(550,250,159,200),"总里程：");
        GUI.Label(new Rect(700, 250, 159, 200),GlobalManager.Shifting.ToString());

    }
    // Use this for initialization
    void Start () {
        XcenterPosition = GlobalManager.GetTexturePosition(TextureResultBg, true);
        YcenterPosition = GlobalManager.GetTexturePosition(TextureResultBg,false);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
