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
using UnityEngine.SceneManagement;//异步加载命名空间

public class LevelOneScenesGUI : MonoBehaviour {
    private bool isShowCountdownNum = false;        //是否显示倒计时
    private int countDownNum;                       //倒计时数字
    public Texture2D texture2DNum3;                 //倒计时数字3贴图
    public Texture2D texture2DNum2;                 //倒计时数字2贴图
    public Texture2D texture2DNum1;                 //倒计时数字1贴图
    public Texture2D textureGameOver;               //游戏结束贴图

    private float num1HorizonPosition;                    //倒计时数字1贴图水平位置                        
    private float num1VerticalPosition;                   //倒计时数字1贴图垂直位置
    private float num2HorizonPosition;                    //倒计时数字1贴图水平位置                        
    private float num2VerticalPosition;                   //倒计时数字1贴图垂直位置
    private float num3HorizonPosition;                    //倒计时数字1贴图水平位置                        
    private float num3VerticalPosition;                   //倒计时数字1贴图垂直位置
    private float gameOverHorizonPosition;                //游戏结束贴图水平位置                        
    private float gameOverVerticalPosition;               //游戏结束贴图垂直位置

    private string texture2DName;                   //倒计时数字贴图名称
    private string strGameOverScene;                //结算场景名称

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
        if (GlobalManager.GlGameState==EnumGameState.End) {
            GUI.DrawTexture(new Rect(gameOverHorizonPosition, gameOverVerticalPosition, textureGameOver.width, textureGameOver.height), textureGameOver);
        }
    }

    
    /// <summary>
    /// 获取贴图中央位置
    /// </summary>
    /// <param name="textureNum">传入贴图</param>
    /// <param name="isX">true求X轴水平位置,false求Y轴</param>
    /// <returns>返回坐标值</returns>
    private int GetTexturePosition(Texture2D textureNum,bool isX) {
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
    private void Awake()
    {
        strGameOverScene = "3_GameOver";
    }
    void Start()
    {   //设置倒计时贴图位置
        num1HorizonPosition = GetTexturePosition(texture2DNum1, true);
        num1VerticalPosition = GetTexturePosition(texture2DNum1, false);

        num2HorizonPosition = GetTexturePosition(texture2DNum2, true);
        num2VerticalPosition = GetTexturePosition(texture2DNum2, false);

        num3HorizonPosition = GetTexturePosition(texture2DNum3, true);
        num3VerticalPosition = GetTexturePosition(texture2DNum3, false);

        gameOverHorizonPosition = GetTexturePosition(textureGameOver, true);
        gameOverVerticalPosition = GetTexturePosition(textureGameOver, false);
        //执行协程
        StartCoroutine("Countdown");
        StartCoroutine("GameStateCheck");
    }


    IEnumerator GameStateCheck() {
        yield return new WaitForSeconds(1f);
        Debug.Log("游戏状态等待一秒");
        while (true) {
            Debug.Log("进入while循环开始执行等待一秒");
            yield return new WaitForSeconds(1f);
            Debug.Log("while循环等待一秒已到开始判定游戏状态");
            if (GlobalManager.GlGameState == EnumGameState.End)
            {
                Debug.Log("游戏状态="+GlobalManager.GlGameState.ToString());

                yield return new WaitForSeconds(2f);
                Debug.Log("等待2秒结束，开始加载结算界面");
                SceneManager.LoadScene(strGameOverScene);
            }
        }
        
        
    }
}
