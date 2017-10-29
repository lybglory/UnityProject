/***
*	title:跑酷项目开发
*	[副标题]
*	Description：第一关卡
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOne_Manager : MonoBehaviour {
    /// <summary>
    /// 持有音频组件
    /// </summary>
    private AudioSource aduioLevelOne;      
    /// <summary>
    /// 结算场景名称
    /// </summary>
    private string strGameOverScene;        
    /// <summary>
    /// 预制体原型
    /// </summary>
    public GameObject ObjPrefaOriginal;     
    /// <summary>
    /// 得到动态创建道具的游戏对象.调用该对象挂载脚本的方法
    /// </summary>
    public GameObject ObjGetCreateOriginal;
    
    /// <summary>
    /// 左桥参考对象数组
    /// </summary>
    public GameObject[] ObjLeftReference;
    /// <summary>
    /// 北桥参考对象数组
    /// </summary>
    public GameObject[] ObjNorthReference;
    /// <summary>
    /// 右桥参考对象数组
    /// </summary>
    public GameObject[] ObjRightReference;
    /// <summary>
    /// 南桥参考对象数组
    /// </summary>
    public GameObject[] ObjSourthReference;



    private void Awake()
    {
        aduioLevelOne=GameObject.Find("_LevelOneAudioManager/LevelOneAudio").GetComponent<AudioSource>();
        strGameOverScene = "3_GameOver";
    }
    // Use this for initialization
    void Start () {
        GlobalManager.GlGameState = EnumGameState.Ready;
        //执行协程，统计里程
        StartCoroutine("GameStateCheck");
        //动态生成道具1秒钟生成一次
        InvokeRepeating("DynamicCreateAllPrefabs", 3f,10f);
        //Debug.Log("ObjLeft的Y轴坐标"+ ObjLeftReference[0].transform.position.y);
        aduioLevelOne = GameObject.Find("_LevelOneAudioManager/LevelOneAudio").GetComponent<AudioSource>();
        aduioLevelOne.Play();
        aduioLevelOne.loop = true;              //开启循环播放
        //根据开始场景保存下来的全局静态音量枚举设置音量大小
        switch (GlobalManager.GlVol) {
            case EnumVolume.MaxVolu:
                aduioLevelOne.volume = 1;
                break;
            case EnumVolume.NormalVolu:
                aduioLevelOne.volume = 0.5f;
                break;
            case EnumVolume.MinVolu:
                aduioLevelOne.volume = 0;
                break;
            case EnumVolume.None:
                aduioLevelOne.volume = 0;
                break;
        }
      
    }
	
	// Update is called once per frame
	void Update () {

    }


    /// <summary>
    /// 游戏状态检查，统计里程
    /// </summary>
    /// <returns>间隔1s执行</returns>
    IEnumerator GameStateCheck()
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("游戏状态等待一秒");
        while (true)
        {
            //Debug.Log("进入while循环开始执行等待一秒");
            yield return new WaitForSeconds(1f);
            if (GlobalManager.GlGameState==EnumGameState.Playing) {
                //全局里程
                ++GlobalManager.Shifting;
            }
            

            //Debug.Log("while循环等待一秒已到开始判定游戏状态");
            if (GlobalManager.GlGameState == EnumGameState.End)
            {
                //Debug.Log("游戏状态="+GlobalManager.GlGameState.ToString());
                yield return new WaitForSeconds(2f);
                //Debug.Log("等待2秒结束，开始加载结算界面");
                SceneManager.LoadScene(strGameOverScene);
            }
        }
    }

    /// <summary>
    /// 动态生成道具的方法
    /// </summary>
    /// <param name="cloneOriginObj">克隆对象的原型</param>
    /// <param name="cloneNum">克隆数量</param>
    /// <param name="destoryTime">销毁时间</param>
    /// <param name="ObjReference">传入参考对象数组</param>
    private void SendMessgObj(GameObject cloneOriginObj,int cloneNum,int destoryTime,GameObject[] ObjReference) {
        /*  确定9个参数：
            原型
            红宝石x坐标最小值，最大值，
            y坐标(定值)
            z坐标min、max。(就是以玩家为参考的坐标)
            克隆数量
            销毁时间
            南桥还是北桥
         */
        System.Object[] ObjArray = new System.Object[9];
        ObjArray[0] = cloneOriginObj;
        ObjArray[3] = ObjReference[0].transform.position.y;
        //如果左桥参考对象
        if (ObjReference == ObjLeftReference|| ObjReference == ObjRightReference) {
            ObjArray[1] = ObjReference[0].transform.position.x;
            ObjArray[2] = ObjReference[1].transform.position.x;
            ObjArray[4] = ObjReference[2].transform.position.z;
            ObjArray[5] = ObjReference[3].transform.position.z;
            ObjArray[8] = true;
        } else if (ObjReference == ObjNorthReference|| ObjReference == ObjSourthReference) {
            //北桥参考对象
            ObjArray[1] = ObjReference[0].transform.position.z;
            ObjArray[2] = ObjReference[1].transform.position.z;
            ObjArray[4] = ObjReference[2].transform.position.x;
            ObjArray[5] = ObjReference[3].transform.position.x;
            ObjArray[8] = false;
        }
        
        ObjArray[6] = cloneNum;
        ObjArray[7] = destoryTime;

        //发送数据
        ObjGetCreateOriginal.SendMessage("CreateDiamondPrefab", ObjArray,SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// 创建场景所有道具，调用SendMessgObj方法
    /// </summary>
    private void DynamicCreateAllPrefabs() {
        //根据里程来创建
        if (GlobalManager.Shifting == 0)
        {
            SendMessgObj(ObjPrefaOriginal, 2, 10, ObjLeftReference);

        }
        else if (GlobalManager.Shifting > 3 && GlobalManager.Shifting < 15)
        {
            SendMessgObj(ObjPrefaOriginal, 5, 15, ObjLeftReference);
            //Debug.Log("创建出来道具的z轴坐标：" + ObjPrefaOriginal.transform.position.z);
        }
        //北桥道具
        else if (GlobalManager.Shifting > 15 && GlobalManager.Shifting < 30) {
            SendMessgObj(ObjPrefaOriginal, 10, 15, ObjNorthReference);
        } else if (GlobalManager.Shifting > 30 && GlobalManager.Shifting < 50) {
            //右桥道具
            SendMessgObj(ObjPrefaOriginal, 10, 20, ObjRightReference);
        }
        else if (GlobalManager.Shifting > 50 && GlobalManager.Shifting < 65)
        {   //南桥道具
            SendMessgObj(ObjPrefaOriginal, 10, 20, ObjSourthReference);
        }
    }

}
