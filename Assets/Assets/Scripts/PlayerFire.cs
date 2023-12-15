using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{

    public GameObject firePosition;

    public GameObject bombDestroy;

    public float throwPower = 15f;

    public GameObject bulletEffect;

    ParticleSystem ps;

    // �߻� ���� ���ݷ�
    public int weaponPower = 5;

    // �ִϸ����� ����
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // �ǰ� ����Ʈ ������Ʈ���� ��ƼŬ �ý��� ������Ʈ ��������
        ps = bulletEffect.GetComponent<ParticleSystem>();

        // �ִϸ����� ��������
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

        if (Input.GetMouseButtonDown(1))
        {
            GameObject bomb = Instantiate(bombDestroy);
            bomb.transform.position = firePosition.transform.position;

            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * throwPower, ForceMode.Impulse);
        }

        if (Input.GetMouseButtonDown(0))
        {
            // ���� �̵� ���� Ʈ�� �Ķ���� ���� 0 �̶��, ���� �ִϸ��̼��� �ǽ��Ѵ�.
            if (anim.GetFloat("MoveMotion") == 0)
            {
                anim.SetTrigger("Attack");
            }
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = new RaycastHit();


            // ���̸� �߻��� �� ���� �ε��� ��ü�� ������ �ǰ� ����Ʈ�� ǥ���Ѵ�.
            if (Physics.Raycast(ray, out hitInfo))
            {
                // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵���Ų��.
                bulletEffect.transform.position = hitInfo.point;

                //ps.Play();

                // �ǰ� ����Ʈ�� forward ������ ���̰� �ε��� ������ ���� ���Ϳ� ��ġ��Ų��.
                bulletEffect.transform.forward = hitInfo.normal;
                // �ǰ� ����Ʈ�� �÷����Ѵ�.
                ps.Play();

            }
            // ���̸� �߻��ϰ�, ���� �ε��� ��ü�� ������...
            if (Physics.Raycast(ray, out hitInfo))
            {
                // ���� ���̿� �ε��� ����� ���̾ ��Enemy����� ������ �Լ��� �����Ѵ�.
                if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    EnemyFSM eFSM = hitInfo.transform.GetComponent<EnemyFSM>();
                    eFSM.HitEnemy(weaponPower);
                }
                // �׷��� �ʴٸ�, ���̿� �ε��� ������ �ǰ� ����Ʈ�� �÷����Ѵ�.
                else
                {
                    // �ǰ� ����Ʈ�� ��ġ�� ���̰� �ε��� �������� �̵���Ų��.
                    bulletEffect.transform.position = hitInfo.point;
                }
            }
        }
    }
}
