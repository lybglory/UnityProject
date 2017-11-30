using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarMaskController : MonoBehaviour {
    public Animator PlAnimator;         //持有Animator句柄
    void Start () {
        PlAnimator = transform.GetComponent<Animator>();
        //当前动画状态机(AnimatorController)的层的数量
        if (PlAnimator.layerCount>=2) {
            //设置状态机的Layer的权重.0表示不受该层影响,
            //1表示没有屏蔽骨骼的部分受到该层骨骼动画控制.
            PlAnimator.SetLayerWeight(1, 1);
        }
    }

	void Update () {
        if (PlAnimator) {
            if (Input.GetButtonDown("Jkey")) {
                PlAnimator.SetBool("IsRoll", true);
            }
            if (Input.GetButtonUp("Jkey")) {
                PlAnimator.SetBool("IsRoll", false);
            }
        }
    }//Update()_end
}//class_end
