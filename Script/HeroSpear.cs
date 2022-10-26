using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpear : HeroComponent
{
    public override void Attack(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;             //방향
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;               //각도

        isAttack = true;
        GameObject wp = Instantiate(weapon, transform.position, Quaternion.Euler(0, 0, angle));
        StartCoroutine(AttackTimer());

    }
}
