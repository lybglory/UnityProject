using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPHealth : MonoBehaviour {
    /// <summary>
    /// 血条句柄
    /// </summary>
    public SliderHP healthSliderHp;
    /// <summary>
    /// 血量
    /// </summary>
    public uint hpVale=1;


	// Use this for initialization
	void Start () {
        healthSliderHp = this.GetComponentInChildren<SliderHP>();
        HpHealthInit();
    }
	
    private void HpHealthInit() {
        healthSliderHp.HpValue = hpVale;
    }
    /// <summary>
    /// 减血的方法
    /// </summary>
    /// <param name="flDamage">受到的伤害值</param>
    public void AcceptDamage(float flDamage) {
        healthSliderHp.HpValue -= flDamage;
    }
}
