using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 1f;

    CharacterController cc;

    float gravity = -10f;
    float yVelocity = 0f;

    public float jumpPower = 10f;

    public bool isJumping = false;

    // 현재 체력
    public int hp = 20;
    // 최대 체력
    public int maxHp = 20;
    // hp 슬라이더 변수
    public Slider hpSlider;

    public GameObject hitEffect;

    // 애니메이터 변수
    Animator anim;

    public void DamageAction(int damage)
    {
        // 에네미이 공격력만큼 hp 깎음.
        hp -= damage;

        if (hp > 0)
        {
            StartCoroutine(PlayerHitEffect());
        }

        // 피격 효과 코루틴 함수
        IEnumerator PlayerHitEffect()
        {
            // 1. 피격 UI를 활성화한다.
            hitEffect.SetActive(true);
            // 2. 0.3초간 대기한다.
            yield return new WaitForSeconds(0.3f);
            // 3. 피격 UI를 비활성화한다.
            hitEffect.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        // 애니메이터 받아오기
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // 게임 상태가 ‘게임 중’ 상태일 때만 조작할 수 있게 한다.
        if (GameManager.Instance.gState != GameManager.GameState.Run)
        {
            return;
        }

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // 이동 블렌딩 트리를 호출하고 벡터의 크기 값을 넘겨준다
        anim.SetFloat("MoveMotion", dir.magnitude);

        dir = Camera.main.transform.TransformDirection(dir);

        if (cc.collisionFlags == CollisionFlags.Below)
        {
            if (isJumping)
            {
                isJumping = false;
            }

            yVelocity = 0f;
        }

        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }


        yVelocity += gravity * Time.deltaTime;
        dir.y += yVelocity;

        cc.Move(dir * moveSpeed * Time.deltaTime);

        // 4. 현재 플레이어 hp(%)를 hp 슬라이더의 value에 반영한다.
        hpSlider.value = (float)hp / (float)maxHp;
    }
}
