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
    /// 参考X坐标最小值
    /// </summary>
    public GameObject ObjLef;
    /// <summary>
    /// 参考X坐标最大值
    /// </summary>
    public GameObject ObjRight;

    /// <summary>
    /// 参考Z坐标最小值
    /// </summary>
    public GameObject ObjMinZ;
    /// <summary>
    /// 参考Z坐标最大值
    /// </summary>
    public GameObject ObjMaxZ;

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
        InvokeRepeating("DynamicCreateAllPrefabs", 1f,1f);
        Debug.Log("ObjLeft的Y轴坐标"+ObjLef.transform.position.y);
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
    /// 数据传值,动态生成道具
    /// </summary>
    /// <param name="cloneOriginObj">预设原型</param>
    /// <param name="cloneNum">克隆的数量</param>
    /// <param name="destoryTime">销毁时间</param>
    private void SendMessgObj(GameObject cloneOriginObj,int cloneNum,int destoryTime) {
        /*  确定8个参数：
            原型
            红宝石x坐标最小值，最大值，
            y坐标(定值)
            z坐标min、max。(就是以玩家为参考的坐标)
            克隆数量
            销毁时间
         */
        System.Object[] ObjArray = new System.Object[8];
        ObjArray[0] = cloneOriginObj;
        ObjArray[1] = ObjLef.transform.position.x;
        ObjArray[2] = ObjRight.transform.position.x;
        ObjArray[3] = ObjLef.transform.position.y;
        ObjArray[4] = ObjMinZ.transform.position.z;
        ObjArray[5] = ObjMaxZ.transform.position.z;
        ObjArray[6] = cloneNum;
        ObjArray[7] = destoryTime;

        //发送数据
        ObjGetCreateOriginal.SendMessage("CreateDiamondPrefab", ObjArray,SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// 创建道具，调用SendMessgObj方法
    /// </summary>
    private void DynamicCreateAllPrefabs() {
        //根据里程来创建
        if (GlobalManager.Shifting == 0) {
            SendMessgObj(ObjPrefaOriginal, 10, 30);
            
        }
        else if (GlobalManager.Shifting >= 14&& GlobalManager.Shifting<31)
        {
            SendMessgObj(ObjPrefaOriginal, 20, 30);
            //Debug.Log("创建出来道具的z轴坐标：" + ObjPrefaOriginal.transform.position.z);
        } else if (GlobalManager.Shifting == 40) {
            SendMessgObj(ObjPrefaOriginal, 30, 40);
            //Debug.Log("创建出来道具的z轴坐标：" + ObjPrefaOriginal.transform.position.z);
        }
    }
    

}
