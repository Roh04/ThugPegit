using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    public EnemyFSM efsm;

    // �÷��̾�� �������� ������ ���� �̺�Ʈ �Լ�
    public void PlayerHit()
    {
        efsm.AttackAction();
    }
}
