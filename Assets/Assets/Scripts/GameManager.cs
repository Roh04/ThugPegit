using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �̱��� ����
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
        // �ʱ� ���� ���´� �غ� ���·� �����Ѵ�.
        gState = GameState.Ready;

        gameText = gameLabel.GetComponent<Text>();

        gameText.text = "Ready...";

        gameText.color = new Color32(255, 185, 0, 255);

        // �÷��̾� ������Ʈ�� ã�� �� �÷��̾��� PlayerMove ������Ʈ �޾ƿ���
        player = GameObject.Find("Player").GetComponent<PlayerMove>();

        StartCoroutine(ReadyToStart());
    }

    IEnumerator ReadyToStart()
    {
        // 2�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(2f);
        // ���� �ؽ�Ʈ�� ������ ��Go!���� �Ѵ�.
        gameText.text = "Go!";
        // 0.5�ʰ� ����Ѵ�.
        yield return new WaitForSeconds(0.5f);
        // ���� �ؽ�Ʈ�� ��Ȱ��ȭ�Ѵ�.
        gameLabel.SetActive(false);
        // ���¸� ������ �ߡ� ���·� �����Ѵ�.
        gState = GameState.Run;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.hp <= 0)
        {
            // �÷��̾��� �ִϸ��̼� ����
            player.GetComponentInChildren<Animator>().SetFloat("MoveMotion", 0);

            gameLabel.SetActive(true);
            gameText.text = "GameOver";
            gameText.color = new Color32(255, 0, 0, 255);
            gState = GameState.GamaOver;
        }
    }
}
