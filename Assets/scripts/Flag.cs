using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour {

    Scene scene;
    public int wait_time = 1;
    bool reached = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(reached == false)
        {
            reached = true;
            StartCoroutine(changeScene());
        }
        
    }

    IEnumerator changeScene()
    {
        FindObjectOfType<AudioManager>().Play("coin");
        yield return new WaitForSeconds(wait_time);
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
