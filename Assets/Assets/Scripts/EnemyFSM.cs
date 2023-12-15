using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }

    EnemyState m_State;

    //�÷��̾� �߰� ����
    public float findDistance = 8f;

    //�÷��̾� Ʈ������
    Transform player;

    public float attackDistance = 2f;

    public float moveSpeed = 5f;

    CharacterController cc;

    float currentTime = 0;

    float attackDelay = 2f;

    public int attackPower = 3;

    // �ʱ� ��ġ ����� ����
    Vector3 originPos;
    Quaternion originRot;

    public float moveDistance = 20f;

    public int hp = 15;
    public int maxHp = 15;

    // ���ʹ��� hp Slider ����
    public Slider hpSlider;

    // ���ϸ����� ����
    public Animator anim;

    // �׺���̼� ������Ʈ ����
    NavMeshAgent smith;

    // Start is called before the first frame update
    void Start()
    {
        // ������ ���´� ���
        m_State = EnemyState.Idle;

        //�÷��̾� Ʈ������ ������Ʈ ��������
        player = GameObject.Find("Player").GetComponent<Transform>();

        // ĳ���� ��Ʈ�ѷ� ������Ʈ �޾ƿ���
        cc = GetComponent<CharacterController>();

        // �ڽ��� �ʱ� ��ġ �����ϱ�
        originPos = transform.position;

        // �ڽ��� �ʱ� ȸ����
        originRot = transform.rotation;

        anim = transform.GetComponentInChildren<Animator>();

        // ������̼� ������Ʈ ������Ʈ �޾ƿ���
        smith = GetComponent<NavMeshAgent>();

     
    }


    void Idle()
    {
        // �÷��Ӿ���� �Ÿ��� �׼� ���� ���� �̳���� Move ���·� ��ȯ
        if(Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Idle -> Move");

            anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        // ���� ���� ��ġ�� �ʱ� ��ġ���� �̵� ���� ������ �Ѿ�ٸ�
        /*if(Vector3.Distance(transform.position, originPos) > moveDistance)
        {
            // ����ġ���� 20���� ũ��
            // ���� ���¸� ����(Return)�� ��ȯ�Ѵ�.
            m_State = EnemyState.Return;
            print("���� ��ȯ: Move -> Return");
        }*/
        // ���� �÷��̾���� �Ÿ��� ���� ���� ���̶�� �÷��̾ ���� �̵��Ѵ�.
        if(Vector3.Distance(transform.position, player.position) > attackDistance)
        {/*
            // ����ġ���� 20���� ����
            // �÷��̾���� �Ÿ��� 2���� ŭ

            // �̵����� ��ȯ
            Vector3 dir = (player.position - transform.position).normalized;

            // ĳ���� ��Ʈ�ѷ� �̿��� �̵�
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // �÷��̾ �ٶ󺸵���
            transform.forward = dir;*/

            // ������̼� ������Ʈ�� �̵��� ���߰� ��� �ʱ�ȭ
            smith.isStopped = true;
            smith.ResetPath();

            // ������̼����� �����ϴ� �ּ� �Ÿ��� ���� ���� �Ÿ��� ����
            smith.stoppingDistance = attackDistance;

            // ������̼� �������� �÷��̾� ��ġ�� ����
            smith.destination = player.position;
        }

        else
        {
            // 2���� ������
            m_State = EnemyState.Attack;
            print("���� ��ȯ: Move -> Attack");

            // ���� �ð��� ���� ������ �ð���ŭ �̸� ������� ���´�.
            currentTime = attackDelay;

            // ���� ��� �ִϸ��̼����� ��ȯ
            anim.SetTrigger("MoveToAttackDelay"); 
        }
    }

    void Attack()
    {
        if(Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // ������ �ð����� player ����
            currentTime += Time.deltaTime;
            if(currentTime > attackDelay)
            {
                // player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("����");
                currentTime = 0;

                // ���� �ִϸ��̼� �ߵ�
                anim.SetTrigger("StartAttack");
            }
        }
        else
        {
            m_State = EnemyState.Move;
            print("���� ��ȯ: Attack -> Move");
            currentTime = 0;

            // ���� -> �߰� �ִϸ��̼� ��ȯ
            anim.SetTrigger("AttackToMove");
        }
    }

    // �÷��̾��� ��ũ��Ʈ�� ������ ó�� �Լ� ����
    public void AttackAction()
    {
        player.GetComponent<PlayerMove>().DamageAction(attackPower);
    }

    /*void Return()
    {
        // ���� �ʱ� ��ġ������ �Ÿ��� 0.1f �̻��̶�� �ʱ� ��ġ ������ �̵��Ѵ�.
        if(Vector3.Distance(transform.position, originPos) > 0.1f)
        {*//*
            Vector3 dir = (originPos - transform.position).normalized;
            cc.Move(dir * moveSpeed * Time.deltaTime);

            // ���� �������� ������ ��ȯ
            transform.forward = dir;*//*

            // ������̼��� �������� �ʱ� ������ ��ġ�� ����
            smith.destination = originPos;

            // ������̼����� �����ϴ� �ּ� �Ÿ��� 0���� ����
            smith.stoppingDistance = 0;
        }
        // �׷��� �ʴٸ�, �ڽ��� ��ġ�� �ʱ� ��ġ�� �����ϰ� ���� ���¸� ���� ��ȯ�Ѵ�.
        else
        {
            // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
            smith.isStopped = true;
            smith.ResetPath();

            transform.position = originPos;
            transform.rotation = originRot;

            // hp�� �ٽ� ȸ���Ѵ�.
            hp = maxHp;
            m_State = EnemyState.Idle;
            print("���� ��ȯ: Return -> Idle");

            // ��� �ִϸ��̼����� ��ȯ�ϴ� Ʈ�������� ȣ��
            anim.SetTrigger("MoveToIdle");
        }
    }*/
    void Damaged()
    {
        // �ǰ� ���¸� ó���ϱ� ���� �ڷ�ƾ�� �����Ѵ�.
        StartCoroutine(DamageProcess());
    }
    // ������ ó���� �ڷ�ƾ �Լ�
    IEnumerator DamageProcess()
    {
        // �ǰ� ��� �ð���ŭ ��ٸ���.
        yield return new WaitForSeconds(1f);
        // ���� ���¸� �̵� ���·� ��ȯ�Ѵ�.
        m_State = EnemyState.Move;
        print("���� ��ȯ: Damaged -> Move");
    }
    
    // ������ ���� �Լ�
    public void HitEnemy(int hitPower)
    {
        // ����, �̹� �ǰ� �����̰ų� ��� ���� �Ǵ� ���� ���¶�� �ƹ��� ó���� ���� �ʰ� �Լ��� �����Ѵ�.
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }
        // �÷��̾��� ���ݷ¸�ŭ ���ʹ��� ü���� ���ҽ�Ų��
        hp -= hitPower;

        // ������̼� ������Ʈ�� �̵��� ���߰� ��θ� �ʱ�ȭ
        smith.isStopped = true;
        smith.ResetPath();

        // ���ʹ��� ü���� 0���� ũ�� �ǰ� ���·� ��ȯ�Ѵ�.
        if (hp > 0)
        {
            m_State = EnemyState.Damaged;
            print("���� ��ȯ : Any State -> Damaged");
            Damaged();

            // �ǰ� �ִϸ��̼� ����
            anim.SetTrigger("Damaged");
        }
        // �׷��� �ʴٸ� ���� ���·� ��ȯ�Ѵ�.
        else
        {
            m_State = EnemyState.Die;
            print("���� ��ȯ : Any State -> Die");
            Die();

            // ���� �ִϸ��̼� ����
            anim.SetTrigger("Die");
        }
    }

    void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }

    IEnumerator DieProcess()
    {
        cc.enabled = false;

        yield return new WaitForSeconds(2f);
        print("�Ҹ�!");
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���¸� üũ�� �ش� ���º��� ������ ����� �����ϰ� �ʹ�.
        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            /*case EnemyState.Return:
                Return();
                break;*/
            case EnemyState.Damaged:
                // Damaged();
                break;
            case EnemyState.Die:
                // Die();
                break;
        }

        // ���� hp(%)�� hp �����̴��� value�� �ݿ��Ѵ�.
        hpSlider.value = (float)hp / (float)maxHp;
    }
}
