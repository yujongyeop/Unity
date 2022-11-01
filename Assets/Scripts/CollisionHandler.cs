using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    AudioSource audioSource;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

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

    //충돌 시 처리
    void StartCrashSequence()
    {
        //실패 효과음 재생
        audioSource.PlayOneShot(crash);
        // Movement 스크립트 비활성화(로켓 제어권 회수)
        GetComponent<Movement>().enabled = false;
        // 1초 지연 후 ReloadLevel 호출
        Invoke("ReloadLevel", levelLoadDelay);
    }

    //스테이지 클리어 시 처리
    void StartSuccessSequence(){
        //성공 효과음 재생
        audioSource.PlayOneShot(success);
        // Movement 스크립트 비활성화(로켓 제어권 회수)
        GetComponent<Movement>().enabled = false;
        // 1초 지연 후 LoadNextLevel 호출
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    //현재 씬 재시작
    void ReloadLevel()
    {
        // 현재 씬의 인덱스 번호를 가져옴
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // 현재 씬을 
        SceneManager.LoadScene(currentSceneIndex);
    }

    //다음 씬으로 이동
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
