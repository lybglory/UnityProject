using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAddScripts : MonoBehaviour {

    public GameObject ObjNeed;
    private void OnTriggerEnter(Collider other)
    {
        if (ObjNeed!=null) {
            ObjNeed.AddComponent<ThornStandMoving>();
        }
    }
}
