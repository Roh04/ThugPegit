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

    // ���� ü��
    public int hp = 20;
    // �ִ� ü��
    public int maxHp = 20;
    // hp �����̴� ����
    public Slider hpSlider;

    public GameObject hitEffect;

    // �ִϸ����� ����
    Animator anim;

    public void DamageAction(int damage)
    {
        // ���׹��� ���ݷ¸�ŭ hp ����.
        hp -= damage;

        if (hp > 0)
        {
            StartCoroutine(PlayerHitEffect());
        }

        // �ǰ� ȿ�� �ڷ�ƾ �Լ�
        IEnumerator PlayerHitEffect()
        {
            // 1. �ǰ� UI�� Ȱ��ȭ�Ѵ�.
            hitEffect.SetActive(true);
            // 2. 0.3�ʰ� ����Ѵ�.
            yield return new WaitForSeconds(0.3f);
            // 3. �ǰ� UI�� ��Ȱ��ȭ�Ѵ�.
            hitEffect.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();

        // �ִϸ����� �޾ƿ���
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ������ �ߡ� ������ ���� ������ �� �ְ� �Ѵ�.
        if (GameManager.Instance.gState != GameManager.GameState.Run)
        {
            return;
        }

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        // �̵� ���� Ʈ���� ȣ���ϰ� ������ ũ�� ���� �Ѱ��ش�
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

        // 4. ���� �÷��̾� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;
    }
}
