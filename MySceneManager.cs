using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MySceneManager : MonoBehaviour {

    public Image black;
    public Animator anim;
    
    //input for index of the level to load
    public void SceneToLoad(int a) {
        
        SceneManager.LoadScene(a);
    }

    public void SceneFade(int a) {
        StartCoroutine(FadeScene(a));
    }
    private IEnumerator FadeScene(int a) {
        anim.SetBool("Fade", true);
        yield return new WaitUntil(() => black.color.a == 1);
        SceneManager.LoadScene(a);
    }

}
