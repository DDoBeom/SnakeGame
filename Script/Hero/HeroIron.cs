using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroIron : HeroComponent
{
    public override void Attack(Vector3 pos)
    {
        isAttack = true;

        GameObject wea = Instantiate(weapon, transform.position, Quaternion.identity);
        wea.GetComponent<ProjectileComponent>().Move(pos);

        StartCoroutine(AttackTimer());
    }
}
