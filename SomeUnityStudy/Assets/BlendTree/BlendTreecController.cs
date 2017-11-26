using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendTreecController : MonoBehaviour {
    public Animator BlAnimator;

    public float FlDirectionDampTime = 0.25f;
    //public float FlLeftSpeed = 0.35f;
    public float FlHorizonalSpeed = 0.35f;
    public float FlForwardSpeed = 0.6f;
    public float FlBackSpeed = 0.55f;
    private float flHorizonal;
    private float flVeritcal;
	// Use this for initialization
	void Start () {
        BlAnimator = transform.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        if (BlAnimator) {
            AnimatorStateInfo stateInfo = BlAnimator.GetCurrentAnimatorStateInfo(0);
            flHorizonal = Input.GetAxis("Horizontal");
             flVeritcal = Input.GetAxis("Vertical");
            Debug.Log("Horizontal="+flHorizonal + ";Vertical=" + flVeritcal);
            BlAnimator.SetFloat("Speed", flHorizonal * flHorizonal + flVeritcal * flVeritcal);
            BlAnimator.SetFloat("Direction", flVeritcal, FlDirectionDampTime, Time.deltaTime);
            if (stateInfo.IsName("Blend Tree"))
            {
                //左移动,对应的blendTree里面的临界值
                if (flHorizonal < 0) {
                    //transform.LookAt(new Vector3(transform.position.x + flHorizonal, transform.position.y, transform.position.z + flVeritcal));
                    //transform.Translate(-transform.right * FlHorizonalSpeed * Time.deltaTime);
                    transform.position += -transform.right * FlHorizonalSpeed * Time.deltaTime;
                } else if (flHorizonal > 0) {
                    //右移动，对应的blendTree临界值
                    //transform.LookAt(new Vector3(transform.position.x + flHorizonal, transform.position.y, transform.position.z + flVeritcal));
                    //transform.Translate(transform.right * FlHorizonalSpeed * Time.deltaTime);

                    transform.position += transform.right * FlHorizonalSpeed * Time.deltaTime;
                }
                //向前
                if (flVeritcal > 0) {
                    //transform.LookAt(new Vector3(transform.position.x + flHorizonal, transform.position.y, transform.position.z + flVeritcal));
                    //transform.Translate(transform.forward * FlForwardSpeed * Time.deltaTime);

                    transform.position += transform.forward * FlForwardSpeed * Time.deltaTime;
                } else if (flVeritcal<0) {
                    //transform.LookAt(new Vector3(transform.position.x + flHorizonal, transform.position.y, transform.position.z + flVeritcal));
                    //transform.Translate(-transform.forward * FlBackSpeed * Time.deltaTime);

                    transform.position += -transform.forward * FlBackSpeed * Time.deltaTime;

                }
            }
        }
        
	}
}
