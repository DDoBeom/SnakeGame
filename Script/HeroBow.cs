using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBow : HeroComponent
{
    public override void Attack(Vector3 pos)
    {
        isAttack = true;
        GameObject wp = Instantiate(weapon, transform.position, Quaternion.identity);

        wp.GetComponent<ProjectileComponent>().Move(pos);
        StartCoroutine(AttackTimer());
    }
}
