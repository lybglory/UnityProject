using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoad : MonoBehaviour {
    /// <summary>
    /// Slider句柄
    /// </summary>
    public Slider loadSlider;


    private void Start()
    {
        StartCoroutine("LoadNextScene");
    }

    private void Update()
    {
        loadSlider.value += Time.deltaTime;
        if (loadSlider.value<=0) {
            loadSlider.value += Time.deltaTime;
        }
        Debug.Log("loadSlider.value" + loadSlider.value+";时间："+Time.time);
        
    }

    IEnumerator LoadNextScene() {
        yield return new WaitForSeconds(1f);
        AsyncOperation asyncOperaNext = SceneManager.LoadSceneAsync(GlobalParamater.GlScene.ToString());
        yield return asyncOperaNext;
    }
}
