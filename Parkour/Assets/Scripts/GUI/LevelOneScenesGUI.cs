/***
*	title:跑酷游戏
*	[副标题] 第一关卡GUI
*	    实现显示倒计时功能
*	    
*	Description：
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneScenesGUI : MonoBehaviour {
    private bool isShowCountdownNum = false;        //是否显示倒计时
    private int countDownNum;                       //倒计时数字
    public Texture2D texture2DNum3;                 //倒计时数字3贴图
    public Texture2D texture2DNum2;                 //倒计时数字2贴图
    public Texture2D texture2DNum1;                 //倒计时数字1贴图
    private float num1HorizonPosition;                    //倒计时数字1贴图水平位置                        
    private float num1VerticalPosition;                   //倒计时数字1贴图垂直位置
    private float num2HorizonPosition;                    //倒计时数字1贴图水平位置                        
    private float num2VerticalPosition;                   //倒计时数字1贴图垂直位置
    private float num3HorizonPosition;                    //倒计时数字1贴图水平位置                        
    private float num3VerticalPosition;                   //倒计时数字1贴图垂直位置

    private string texture2DName;                   //倒计时数字贴图名称


    /// <summary>
    /// 倒计时协程
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown(){
        //先要等GUI绘制完毕之后在继续执行
        yield return new WaitForEndOfFrame();

        //数字3
        isShowCountdownNum = true;
        countDownNum = 3;
        yield return new WaitForSeconds(1f);        //倒计时数字为3时候，等待一秒
        isShowCountdownNum = false;
        yield return new WaitForSeconds(0.5f);      //0.5f不显示

        //数字2
        isShowCountdownNum = true;
        countDownNum = 2;
        yield return new WaitForSeconds(1f);        //倒计时数字为3时候，等待一秒
        isShowCountdownNum = false;
        yield return new WaitForSeconds(0.5f);      //0.5f不显示

        //数字1
        isShowCountdownNum = true;
        countDownNum = 1;
        yield return new WaitForSeconds(1f);        //倒计时数字为3时候，等待一秒
        isShowCountdownNum = false;
        yield return new WaitForSeconds(0.5f);      //0.5f不显示

        //倒计时完毕，设置游戏状态，开始游戏
        GlobalManager.GlGameState = EnumGameState.Playing;
    }

    private void OnGUI()
    {   //由这个标志位控制是否需要显示倒计时
        if (isShowCountdownNum) {
            if (countDownNum == 3) {
                GUI.DrawTexture(new Rect(num3HorizonPosition, num3VerticalPosition, texture2DNum3.width, texture2DNum3.height), texture2DNum3);
            } else if (countDownNum == 2) {
                GUI.DrawTexture(new Rect(num2HorizonPosition, num2VerticalPosition, texture2DNum2.width, texture2DNum2.height), texture2DNum2);
            } else if (countDownNum == 1) {
                GUI.DrawTexture(new Rect(num1HorizonPosition, num1VerticalPosition, texture2DNum1.width, texture2DNum1.height), texture2DNum1);
            }
        }
    }

    
    /// <summary>
    /// 获取中央位置
    /// </summary>
    /// <param name="textureNum">传入贴图</param>
    /// <param name="isX">是否求X轴水平位置</param>
    /// <returns>返回坐标值</returns>
    private int GetCountdownPosition(Texture2D textureNum,bool isX) {
        int position;
        //true位求X轴坐标
        if (isX)
        {
            position = (Screen.width - textureNum.width) / 2;
        }
        else {//false返回Y轴坐标
            position = (Screen.height - textureNum.height) / 2;
        }
        return position;

    }

    void Start()
    {   //设置倒计时贴图位置
        num1HorizonPosition = GetCountdownPosition(texture2DNum1, true);
        num1VerticalPosition = GetCountdownPosition(texture2DNum1, false);

        num2HorizonPosition = GetCountdownPosition(texture2DNum2, true);
        num2VerticalPosition = GetCountdownPosition(texture2DNum2, false);

        num3HorizonPosition = GetCountdownPosition(texture2DNum3, true);
        num3VerticalPosition = GetCountdownPosition(texture2DNum3, false);
        //执行协程
        StartCoroutine("Countdown");
    }
}
