using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteraction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched the cube");
            // End the game (you can replace this with your game over logic)
            EndGame();
        }
    }

    void EndGame()
    {
        // You can add your game over logic here
        Debug.Log("Game Over");
        // For example, you might want to load a game over scene or restart the level
        // SceneManager.LoadScene("GameOverScene");
        // Or restart the level
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // For simplicity, we'll just quit the application in this example
        Application.Quit();
    }
}

/*
using UnityEngine.UI; // UI 요소를 사용하기 위함

public class WinTrigger : MonoBehaviour
{
    public Text winText; // 'You Win' 텍스트를 표시할 UI 텍스트 요소

    private void Start()
    {
        winText.enabled = false; // 시작 시 'You Win' 텍스트를 숨깁니다
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 'Player' 태그를 가진 객체와 충돌했는지 확인
        {
            winText.enabled = true; // 'You Win' 텍스트를 활성화하여 표시
            Invoke("EndGame", 2f); // 2초 후에 게임 종료 함수 호출
        }
    }

    void EndGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중이라면 에디터 실행을 멈춥니다
        #else
            Application.Quit(); //
        #endif
    }
}
*/