using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自己写一个相机跟随
/// </summary>
public class SmoothFollow : MonoBehaviour {


    // The target we are following
    public Transform Target;
    // The distance in the x-z plane to the target
    public float distance = 10;
    // the height we want the camera to be above the target
    public float height = 5;
    // How much we 
    public float heightDamping = 2;
    public float rotationDamping = 3;

    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]


    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!Target)
        {
            return;
        }

        //存储目标欧拉角
        float wantedRotationAngle = Target.eulerAngles.y;
        float wantedHeight = Target.position.y + height;
        //存储当前欧拉角
        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = Target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        // Set the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight,transform.position.z);

        // Always look at the target
        transform.LookAt(Target);
    }
    
}
