using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrol_player : MonoBehaviour {
	public AnimationClip[] animationClips;
	private Animation GetAnimation;
    private string[] animationNames=null;
	// Use this for initialization
	void Start () {
		GetAnimation = this.gameObject.GetComponent<Animation>();
        animationNames = new string[animationClips.Length];
        Debug.Log("animationNames.length=" + animationNames.Length);
        Debug.Log("animationClips.length=" + animationClips.Length);
        for (int i = 0; i < animationClips.Length; i++)
        {
            
            string str1 = animationClips[i].name;
            int atIndex = str1.IndexOf('@');
            string clipName = str1.Substring(atIndex + 1);
            Debug.Log(clipName);
            animationNames[i]=clipName;

            Debug.Log(animationNames[i]);
        }
    }
    int rmunm = 0;

    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (rmunm < animationClips.Length)
            {

                Debug.Log("rmunm=" + rmunm);
                GetAnimation.Play(animationNames[rmunm]);
                Debug.Log("playing " + animationNames[rmunm]);
                rmunm += 1;
            }
            else {
                rmunm = 6;
            }

        }

    }
}
