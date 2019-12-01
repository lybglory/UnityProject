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
	// Use this for initialization
	void Start () {
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
}
