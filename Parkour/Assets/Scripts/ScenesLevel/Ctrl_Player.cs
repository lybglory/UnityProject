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

    // Use this for initialization
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
        if (GlobalManager.GlGameState != EnumGameState.Playing) {
            plAnimation.Play("Walking");
        } else if (GlobalManager.GlGameState== EnumGameState.Playing) {
            //plAnimation.Play("Run");
            this.transform.Translate(Vector3.forward * flRunSpeed, Space.Self);//以第一人视角跑动
            //if (Input.GetKey(KeyCode.S))
            //{
            //    this.transform.Translate(Vector3.down * runSpeed, Space.Self);
            //}
            //else if (Input.GetKey(KeyCode.W))
            //{
            //    this.transform.Translate(Vector3.up * runSpeed, Space.Self);
            //} else if (Input.GetKey(KeyCode.A)) {
            //    this.transform.Translate(Vector3.left * runSpeed, Space.Self);
            //} else if (Input.GetKey(KeyCode.D)) {
            //    this.transform.Translate(Vector3.right * runSpeed, Space.Self);
            //}

            if (this.transform.position.z > 93)
            {
                this.transform.position = startPoint;
            }
        }
        
    }//Update()_end

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
                    this.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up* flJumpSpeed, ForceMode.Impulse);
                    GlobalManager.EnumPlAction = EnumPlayerAnima.Jumping;
                    yield return new WaitForSeconds(PlJumpingClip.length);

                }
            }//最外层if_end
        }//while_end
    }//协程_end

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
    /// 切换相机的方法
    /// </summary>
    /// <param name="strCameraTag">相机Tag标签</param>
    /// <param name="StrCameraName">相机名称</param>
    private void ChangeCamera(string strCameraTag,string StrCameraName) {
        GameObject[] ObjCameras = GameObject.FindGameObjectsWithTag(strCameraTag);
    }
}
