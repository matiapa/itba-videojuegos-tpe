using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour {
    public GameObject LoadingScreen;
    public Image LoadingBarFill;

    public void LoadScene(int sceneId) {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId) {

        LoadingScreen.SetActive(true);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneId);

        while(!async.isDone) {
            float fillAmount = Mathf.Clamp01(async.progress / 0.9f);

            LoadingBarFill.fillAmount = fillAmount;
            
            yield return null;
        }
    }

}