using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 싱글톤 변수
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public enum GameState
    {
        Ready,
        Run,
        GamaOver
    }

    public GameState gState;

    public GameObject gameLabel;

    Text gameText;

    PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {
        // 초기 게임 상태는 준비 상태로 설정한다.
        gState = GameState.Ready;

        gameText = gameLabel.GetComponent<Text>();

        gameText.text = "Ready...";

        gameText.color = new Color32(255, 185, 0, 255);

        // 플레이어 오브젝트를 찾은 후 플레이어의 PlayerMove 컴포넌트 받아오기
        player = GameObject.Find("Player").GetComponent<PlayerMove>();

        StartCoroutine(ReadyToStart());
    }

    IEnumerator ReadyToStart()
    {
        // 2초간 대기한다.
        yield return new WaitForSeconds(2f);
        // 상태 텍스트의 내용을 ‘Go!’로 한다.
        gameText.text = "Go!";
        // 0.5초간 대기한다.
        yield return new WaitForSeconds(0.5f);
        // 상태 텍스트를 비활성화한다.
        gameLabel.SetActive(false);
        // 상태를 ‘게임 중’ 상태로 변경한다.
        gState = GameState.Run;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hp <= 0)
        {
            // 플레이어의 애니메이션 멈춤
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0);

            gameLabel.SetActive(true);
            gameText.text = "GameOver";
            gameText.color = new Color32(255, 0, 0, 255);
            gState = GameState.GamaOver;
        }
    }
}
