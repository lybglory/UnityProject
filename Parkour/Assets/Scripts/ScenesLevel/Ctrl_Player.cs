/***
*	title:游戏主角控制类
*	[副标题]
*	Description：
*	[描述]
*	Date：2017
*	Version：0.1
*	Modify Recoder：
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl_Player : MonoBehaviour {
    /// <summary>
    /// 跑步速度
    /// </summary>
    public float flRunSpeed=1f;
    /// <summary>
    /// 转动的速度
    /// </summary>
    public float flRotateSpeed = 3f;
    /// <summary>
    /// 跳跃的速度
    /// </summary>
    public float flJumpSpeed = 4f;
    /// <summary>
    /// 持有Animation组件
    /// </summary>
    private Animation plAnimation; 
    /// <summary>
    /// 起始位置
    /// </summary>
    private Vector3 startPoint;
    /// <summary>
    /// 行走动画剪辑
    /// </summary>
    public AnimationClip PlWalkingiClip;
    /// <summary>
    /// 跑动动画剪辑
    /// </summary>
    public AnimationClip PlRuningClip;
    /// <summary>
    /// 跳跃动画剪辑
    /// </summary>
    public AnimationClip PlJumpingClip;
    /// <summary>
    /// 俯冲动画剪辑
    /// </summary>
    public AnimationClip PlSubductionClip;
    /// <summary>
    /// 跌落动画剪辑
    /// </summary>
    public AnimationClip PlFallingClip;
    /// <summary>
    /// 最远的位置
    /// </summary>
    private float flMaxPoint = 93;
    /// <summary>
    /// 相机Tag名称
    /// </summary>
    private string _strCameraTag = "Cameras";
    /// <summary>
    /// 主相机名称
    /// </summary>
    private string strMainCameraName = "MainCamera";
    /// <summary>
    /// 远相机名称
    /// </summary>
    private string strLongCameraName = "LongCamera";
    /// <summary>
    /// 透视相机名称
    /// </summary>
    private string strPerspactiveCameraName = "PerspactiveCamera";

    void Start () {
        plAnimation=this.gameObject.GetComponent<Animation>();
        startPoint = this.gameObject.transform.position;
        //执行协程
        StartCoroutine("IEInputMonitoring");
        StartCoroutine("IEPlayAnimationClip");
    }
	
	// Update is called once per frame
	void Update () {
        //根据游戏状态，判定玩家是否奔跑。当倒计时结束后玩家才开始奔跑
        if (GlobalManager.GlGameState != EnumGameState.Playing)
        {
            plAnimation.Play("Walking");
        }
        else if (GlobalManager.GlGameState == EnumGameState.Playing)
        {   //以第一人视角跑动
            this.transform.Translate(Vector3.forward * flRunSpeed, Space.Self);
        }
        //回到起点位置
        if (this.transform.position.z > flMaxPoint){
            this.transform.position = startPoint;
        }
        InputChangeCamera();
   
        
    }//Update()_end

    /// <summary>
    /// 协程按键监听
    /// </summary>
    /// <returns></returns>
    IEnumerator IEInputMonitoring()
    {
        //yield return new WaitForSeconds(0.1f);
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            if (GlobalManager.GlGameState == EnumGameState.Playing)
            {
                GlobalManager.EnumPlAction = EnumPlayerAnima.Runing;
                if (Input.GetKey(KeyCode.A))
                {

                    this.transform.Rotate(Vector3.down * flRotateSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    this.transform.Rotate(Vector3.up * flRotateSpeed);
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    //跳跃
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * flJumpSpeed, ForceMode.Impulse);
                    GlobalManager.EnumPlAction = EnumPlayerAnima.Jumping;
                    yield return new WaitForSeconds(PlJumpingClip.length);
                    GlobalManager.EnumPlAction = EnumPlayerAnima.Runing;

                } else if (Input.GetKeyDown(KeyCode.S)) {
                    //Subduction俯冲
                    GlobalManager.EnumPlAction = EnumPlayerAnima.Subduction;
                    yield return new WaitForSeconds(PlSubductionClip.length);
                    GlobalManager.EnumPlAction = EnumPlayerAnima.Runing;
                }
            }//最外层if_end
        }//while_end
    }//协程_end

    /// <summary>
    /// 协程，控制动画剪辑的播放
    /// </summary>
    /// <returns></returns>
    IEnumerator IEPlayAnimationClip (){
        //yield return new WaitForSeconds(0.1f);
        while (true) {
            yield return new WaitForSeconds(0.01f);
            if (GlobalManager.GlGameState==EnumGameState.Playing) {
                switch (GlobalManager.EnumPlAction)
                {
                    case EnumPlayerAnima.None:
                        break;
                    case EnumPlayerAnima.Walking:
                        //plAnimation.Play(PlWalkingiClip.name);
                        break;
                    case EnumPlayerAnima.Runing:
                        plAnimation.Play(PlRuningClip.name);
                        yield return new WaitForSeconds(PlRuningClip.length);
                        break;
                    case EnumPlayerAnima.Jumping:
                        plAnimation.Play(PlJumpingClip.name);
                        yield return new WaitForSeconds(PlJumpingClip.length);
                        break;
                    case EnumPlayerAnima.Subduction:
                        plAnimation.Play(PlSubductionClip.name);
                        yield return new WaitForSeconds(PlSubductionClip.length);
                        break;
                    case EnumPlayerAnima.Falling:
                        break;
                    default:
                        break;
                }
            }
        }
    }//动画协程_end

    /// <summary>
    /// 获得按键输入，切换相机
    /// </summary>
    private void InputChangeCamera() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            ChangeCamera(_strCameraTag, strMainCameraName);
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            ChangeCamera(_strCameraTag, strLongCameraName);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            ChangeCamera(_strCameraTag, strMainCameraName);
            //开启透视相机【只照射dangerlayer层】
            GameObject.Find(strPerspactiveCameraName).GetComponent<Camera>().enabled = true;
        }
    }

    /// <summary>
    /// 切换相机的方法
    /// </summary>
    /// <param name="strCameraTag">相机Tag标签</param>
    /// <param name="strCameraName">需要启用的相机名称</param>
    private void ChangeCamera(string strCameraTag,string strCameraName) {
        GameObject[] ObjCameras = GameObject.FindGameObjectsWithTag(strCameraTag);
        for (int i = 0; i < ObjCameras.Length; i++)
        {   //禁用所有相机对象上的Camera组件
            ObjCameras[i].GetComponent<Camera>().enabled = false;
        }
        GameObject.Find(strCameraName).GetComponent<Camera>().enabled = true;
    }
}
