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
    private AudioSource aduioLevelOne;      //持有音频组件
    private string strGameOverScene;        //结算场景名称
    public GameObject ObjPrefaOriginal;     //预制体原型
    public GameObject ObjGetCreateOriginal;//得到动态创建的游戏对象

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
        //动态生成道具
        InvokeRepeating("",1f,1f);

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
    /// <param name="cloneOriginObj">原型预制体</param>
    /// <param name="maxX">道具最大X坐标</param>
    /// <param name="minX">道具最小X坐标</param>
    /// <param name="y">道具固定y轴坐标</param>
    /// <param name="maxZ">道具最大Z坐标</param>
    /// <param name="minZ">道具最小Z坐标</param>
    /// <param name="cloneNum">克隆道具预设的数量</param>
    /// <param name="destoryTime">道具销毁的时间</param>
    private void SendMessgObj(GameObject cloneOriginObj,int maxX,int minX,int y,int maxZ,int minZ, int cloneNum,int destoryTime) {
        /*  8个参数：
            原型
            红宝石x坐标最小值，最大值，
            y坐标(定值)
            z坐标min、max。
            红宝石数量
            销毁时间
         */
        System.Object[] ObjArray = new System.Object[8];

        //发送数据
        ObjGetCreateOriginal.SendMessage("",ObjArray,SendMessageOptions.DontRequireReceiver);

    }

    /// <summary>
    /// 留坑，动态创建
    /// </summary>
    private void DynamicCreateAllPrefabs() {

    }
    

}
