using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderHP : MonoBehaviour {
    /// <summary>
    /// 血条
    /// </summary>
    [SerializeField]
    private Transform hpFront;
    /// <summary>
    /// 血条私有值
    /// </summary>
    private float m_hpfValue;
    /// <summary>
    /// 血条值属性
    /// </summary>
    public float HpValue {
        get {
            return m_hpfValue;
        }
        set {
            m_hpfValue = value;
            hpFront.localScale = new Vector3(m_hpfValue, 1, 1);
            //localScale.x=0.4  localPosition.x=-0.45  
            //localScale.x=0.5  localPosition.x=-0.37
            //localScale.x=0.6  localPosition.x=-0.29
            //localScale.x=0.7  localPosition.x=-0.21  //步长0.8
            hpFront.localPosition = new Vector3((1 - m_hpfValue) * -0.8f, 0, 0);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
