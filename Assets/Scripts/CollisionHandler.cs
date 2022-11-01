using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;

    //충돌 여부(중복 충돌 방지를 위함)
    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
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
        //충돌 여부를 True 설정
        isTransitioning = true;
        //기존에 재생중이던 소리 멈춤
        audioSource.Stop();
        //실패 효과음 재생
        audioSource.PlayOneShot(crash);
        // Movement 스크립트 비활성화(로켓 제어권 회수)
        GetComponent<Movement>().enabled = false;
        // 1초 지연 후 ReloadLevel 호출
        Invoke("ReloadLevel", levelLoadDelay);
    }

    //스테이지 클리어 시 처리
    void StartSuccessSequence()
    {
        //충돌 여부를 True 설정
        isTransitioning = true;
        //기존에 재생중이던 소리 멈춤
        audioSource.Stop();
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

