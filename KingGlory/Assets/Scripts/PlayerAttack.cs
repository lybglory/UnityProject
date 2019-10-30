using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private Animator plAtkAnimaor;
    /// <summary>
    /// 技能特效句柄
    /// </summary>
    [SerializeField]
    private ParticleSystem effectFire;
	// Use this for initialization
	void Start () {
        plAtkAnimaor = GetComponent<Animator>();

    }

    public void Attack1() {
        plAtkAnimaor.SetInteger("state", PlayerAnimatorState.Attack1);
        
    }

    public void Attack2()
    {
        plAtkAnimaor.SetInteger("state", PlayerAnimatorState.Attack2);
    }

    public void Dance()
    {
        plAtkAnimaor.SetInteger("state", PlayerAnimatorState.Dance);
    }

    /// <summary>
    /// 重置空闲状态
    /// </summary>
    public void ResetIdle() {
        plAtkAnimaor.SetInteger("state", PlayerAnimatorState.Idle);
    }

    public void Effec1() {
        effectFire.Play();
    }
}
