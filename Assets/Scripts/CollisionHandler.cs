using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        // Movement 스크립트 비활성화(로켓 제어권 회수)
        GetComponent<Movement>().enabled = false;
        // 1초 지연 후 ReloadLevel 호출
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence(){
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }


    void ReloadLevel()
    {
        // 현재 씬의 인덱스 번호를 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // 현재 씬을 
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {

        // 현재 씬의 인덱스 번호를 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
