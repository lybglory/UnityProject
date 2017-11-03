using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    void Update(){
        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("按下A键");
            this.gameObject.GetComponent<MonobehaviorTest>().enabled = true;
        }else if (Input.GetKeyDown(KeyCode.D)){
            Debug.Log("按下D键");
            this.gameObject.GetComponent<MonobehaviorTest>().enabled = false;
        }
    }
}
