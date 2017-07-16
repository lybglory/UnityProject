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
using UnityEngine.SceneManagement;//异步加载命名空间
public class ResultGUI : MonoBehaviour {
    public Texture2D TextureRedDiamond;
    public Texture2D TextureBlueDiamond;
    public Texture2D TextureResultBg;
    public GUISkin ResultGUISkin;
    private float XcenterPosition;
    private float YcenterPosition;
    private float _tempShifting=0;                    //局部里程变量
    private bool _isSHowShifting=false;               //是否显示总里程
    private float _tempDiamondNumber = 0;             //局部宝石变量
    private bool _isSHowDiamondNumber = false;        //是否显示总吃到的红宝石
    public string AgainSceneStr;                      //重新开始关卡名称
    private bool IsTryAgain;                          //是否重新开始
    public Texture2D TextureTryAgainBtn;              //重新开始btn贴图
    private float TryAgainBtnHoriPosition;            //重新开始bt水平位置
    private float TryAgainBtnVeriticalPosition;       //重新开始btn垂直问题
    public Texture2D TextureCancel;                   //取消btn贴图
    private float CancelBtnHoriPosition;              //取消bt水平位置
    private float CancelBtnVeriticalPosition;         //取消btn垂直问题
    private bool IsCancel;



    private void OnGUI()
    {   //需要给GUI的皮肤赋值。GUI.Label字体的大小才会生效
        GUI.skin = ResultGUISkin;
        //绘制背景贴图,new Rect（屏幕中间的X轴position，Y轴position，贴图的长度，贴图的宽度）
        GUI.DrawTexture(new Rect(XcenterPosition, YcenterPosition, TextureResultBg.width,TextureResultBg.height), TextureResultBg);
        //绘制路程
        GUI.Label(new Rect(550,250,159,200),"总里程：");
        //控制全局里程的显示
        if (_isSHowShifting) {
            GUI.Label(new Rect(700, 250, 159, 200), _tempShifting.ToString());
        }
        //绘制红宝石贴图
        GUI.DrawTexture(new Rect(550, 300, 50, 50), TextureRedDiamond);
        if (_isSHowDiamondNumber) {
            GUI.Label(new Rect(700, 300, 150, 50), _tempDiamondNumber.ToString());
        }
        IsTryAgain=GUI.Button(new Rect(TryAgainBtnHoriPosition-200,TryAgainBtnVeriticalPosition-150, TextureTryAgainBtn.width, TextureTryAgainBtn.height),"", ResultGUISkin.GetStyle("Btn_TryAgain"));
        IsCancel = GUI.Button(new Rect(CancelBtnHoriPosition + 200, CancelBtnVeriticalPosition - 150, TextureCancel.width, TextureCancel.height), "", ResultGUISkin.GetStyle("Btn_Cancel"));


    }
    // Use this for initialization
    void Start () {
        //调用居中方法，求出位置
        XcenterPosition = GlobalManager.GetTexturePosition(TextureResultBg, true);
        YcenterPosition = GlobalManager.GetTexturePosition(TextureResultBg,false);
        GlobalManager.Shifting = 20;//临时定义全局里程，测试用

        //获取重新开始btn贴图中央位置
        TryAgainBtnHoriPosition = GlobalManager.GetTexturePosition(TextureTryAgainBtn, true);
        TryAgainBtnVeriticalPosition = GlobalManager.GetTexturePosition(TextureTryAgainBtn, false);
        CancelBtnHoriPosition = GlobalManager.GetTexturePosition(TextureCancel, true);
        CancelBtnVeriticalPosition = GlobalManager.GetTexturePosition(TextureCancel, false);

        StartCoroutine("ShowShifting");//执行协程
        StartCoroutine("ShowDiamondsNumber");//执行协程

    }

    void Update()
    {   //一旦获取到istryAgain为true
        if (IsTryAgain) {
            SceneManager.LoadSceneAsync("2_LevelOne");
        }
        //退出游戏
        if (IsCancel) {
            Application.Quit();
            Debug.Log("退出游戏");
        }
    }

    /// <summary>
    /// 协程。全局里程显示
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowShifting()
    {   //先等待5s
        yield return new WaitForSeconds(0.5f);
        while (true) {
            
            if (_tempShifting >=GlobalManager.Shifting-1)
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

    /// <summary>
    /// 协程，显示红宝石数量
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowDiamondsNumber() {
        yield return new WaitForSeconds(0.5f);
        while (true) {
            //一旦累加的红宝石数量等于全局红宝石数量。就停止协程
            if (_tempDiamondNumber>=GlobalManager.DiamondNum-1) {
                StopCoroutine("ShowDiamondsNumber");
            }
            ++_tempDiamondNumber;
            _isSHowDiamondNumber = true;
            yield return new WaitForSeconds(0.2f);
            _isSHowDiamondNumber = false;
            yield return new WaitForSeconds(0.2f);   
        }
        
    }

}
