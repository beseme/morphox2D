using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    float timer = 0.7f;
    bool timerActive = false;
    AsyncOperation scene;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scene = SceneManager.LoadSceneAsync(2);
            scene.allowSceneActivation = false;
            timerActive = true;
        }

        
    }

    private void Update()
    {
        if (timerActive)
            timer -= Time.deltaTime;
        if (timer <= 0)
            scene.allowSceneActivation = true;
    }
}
