using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    /// <summary>
    /// 子弹目标物
    /// </summary>
    private GameObject objBulletTraget;
    /// <summary>
    /// 子弹速度
    /// </summary>
    public int bulletSpeed = 15;
    /// <summary>
    /// 箭塔对象(子弹父对象)
    /// </summary>
    public  Towers bulletParentTower;
	// Use this for initialization
	void Start () {
        bulletParentTower = this.gameObject.GetComponentInParent<Towers>();
        //Debug.Log("bulletParentTower.name=" + bulletParentTower.name);
        Destroy(this.gameObject,1f);
    }
	
	// Update is called once per frame
	void Update () {
        if (objBulletTraget)
        {
            Vector3 vec = objBulletTraget.transform.position - this.transform.position;
            this.GetComponent<Rigidbody>().velocity = vec.normalized * bulletSpeed;
        }
        else {
            Destroy(this.gameObject);
        }
	}

    /// <summary>
    /// 设置子弹目标
    /// </summary>
    /// <param name="objTarget">子弹目标物</param>
    public void SetBulletTarget(GameObject objTarget) {
        objBulletTraget = objTarget;
    }

    /// <summary>
    /// 子弹碰撞(攻击)
    /// </summary>
    /// <param name="other">子弹碰撞物</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Soldier")) {
            //减血,调用减血的方法
            HPHealth hPHealthBullet = other.GetComponent<HPHealth>();
            if (hPHealthBullet) {
                hPHealthBullet.AcceptDamage(0.2f);
                if (hPHealthBullet.healthSliderHp.HpValue <= 0)
                {
                    //从小兵集合队列中移除，再销毁。子弹也要销毁
                    bulletParentTower.lsSoldier.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
            }
            //销毁子弹
            Destroy(this.gameObject);
        } else if (other.gameObject.tag.Equals("Player")) {
            //减血,调用减血的方法
            HPHealth hPHealthBullet = other.GetComponent<HPHealth>();
            if (hPHealthBullet)
            {
                hPHealthBullet.AcceptDamage(0.5f);
                if (hPHealthBullet.healthSliderHp.HpValue <= 0)
                {
                    //从英雄集合队列中移除，再销毁。子弹也要销毁
                    bulletParentTower.lsHero.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
            }
            Destroy(this.gameObject);
        }
    }
}
