using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreecController : MonoBehaviour {
    public Animator BlAnimator;

    public float FlDirectionDampTime = 0.25f;
    public float FlHorizonalSpeed = 0.35f;
    public float FlForwardSpeed = 0.6f;
    public float FlBackSpeed = 0.55f;
    /// <summary>
    /// 存储水平轴的值
    /// </summary>
    private float flHorizonal;
    /// <summary>
    /// 存储垂直轴的值
    /// </summary>
    private float flVeritcal;
	void Start () {
        BlAnimator = transform.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (BlAnimator) {
            AnimatorStateInfo stateInfo = BlAnimator.GetCurrentAnimatorStateInfo(0);
            flHorizonal = Input.GetAxis("Horizontal");
            flVeritcal = Input.GetAxis("Vertical");
            //设置动画过渡
            if (flHorizonal != 0|| flVeritcal!=0){
                BlAnimator.SetBool("IsMove", true);
            }
            else{
                BlAnimator.SetBool("IsMove", false);
            }
            //设置水平的移动的临界值（-1向左，0空闲，1向右）
            BlAnimator.SetFloat("Speed", flHorizonal*1);
            //设置前后移动的临界值
            BlAnimator.SetFloat("Direction", flVeritcal, FlDirectionDampTime, Time.deltaTime);
            Debug.Log("Speed="+ flHorizonal *1+ ";Direction="+flVeritcal);
            if (stateInfo.IsName("Blend Tree"))
            {
                BlAnimator.SetFloat("Speed", flHorizonal * 1);
                BlAnimator.SetFloat("Direction", flVeritcal, FlDirectionDampTime, Time.deltaTime);
                //小于0左移动,对应的blendTree里面的临界值
                Debug.Log("Horizontal=" + flHorizonal + ";Vertical=" + flVeritcal);
                if (flHorizonal < 0) {
                    transform.position += -transform.right * FlHorizonalSpeed * Time.deltaTime;
                } else if (flHorizonal > 0) {
                    //大于0右移动，对应的blendTree临界值
                    transform.position += transform.right * FlHorizonalSpeed * Time.deltaTime;
                }
                
                //前后的位移
                if (flVeritcal > 0) {
                    transform.position += transform.forward * FlForwardSpeed * Time.deltaTime;
                } else if (flVeritcal<0) {
                    transform.position += -transform.forward * FlBackSpeed * Time.deltaTime;
                }
            }
        }
        
	}
}
