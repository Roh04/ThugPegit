using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent : MonoBehaviour
{
    public EnemyFSM efsm;

    // 플레이어에게 데미지를 입히기 위한 이벤트 함수
    public void PlayerHit()
    {
        efsm.AttackAction();
    }
}
