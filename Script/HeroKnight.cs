using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroKnight : HeroComponent
{
    public override void Attack(Vector3 pos)
    {
        isAttack = true;
        GameObject weap = Instantiate(weapon, transform.position, Quaternion.identity);
        weap.transform.SetParent(transform.parent);
        weap.transform.rotation = transform.rotation;

        StartCoroutine(AttackTimer());
    }
}
