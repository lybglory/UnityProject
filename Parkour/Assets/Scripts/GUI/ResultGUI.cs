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
    private float _tempShifting=0;          //局部变量
    private bool _isSHowShifting=false;     //是否显示

    private void OnGUI()
    {   //需要给GUI的皮肤赋值。字体的大小才会生效
        GUI.skin = ResultGUISkin;
        //绘制背景贴图,new Rect（屏幕中间的X轴position，Y轴position，贴图的长度，贴图的宽度）
        GUI.DrawTexture(new Rect(XcenterPosition, YcenterPosition, TextureResultBg.width,TextureResultBg.height), TextureResultBg);
        //绘制路程
        GUI.Label(new Rect(550,250,159,200),"总里程：");
        //控制全局里程的显示
        if (_isSHowShifting) {
            GUI.Label(new Rect(700, 250, 159, 200), _tempShifting.ToString());
        }
        

    }
    // Use this for initialization
    void Start () {
        //调用居中方法，求出位置
        XcenterPosition = GlobalManager.GetTexturePosition(TextureResultBg, true);
        YcenterPosition = GlobalManager.GetTexturePosition(TextureResultBg,false);
        GlobalManager.Shifting = 20;//临时定义全局里程，测试用

        StartCoroutine("ShowShifting");//执行协程

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 协程。全局里程显示
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowShifting()
    {   //先等待5s
        yield return new WaitForSeconds(0.5f);
        while (true) {
            if (_tempShifting >= GlobalManager.Shifting-1)
            {
                StopCoroutine("ShowShifting");
            }
            ++_tempShifting;
            _isSHowShifting = true;
            //显示0.3f
            yield return new WaitForSeconds(0.3f);
            //显示之后，等待0.2f
            _isSHowShifting = false;
            yield return new WaitForSeconds(0.2f);
        }


    }
}
