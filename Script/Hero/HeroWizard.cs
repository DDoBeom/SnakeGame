using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroWizard : HeroComponent
{
    public override void Attack(Vector3 pos)
    {
        isAttack = true;
        GameObject temp = Instantiate(weapon, transform.position, Quaternion.identity);
        temp.GetComponent<ProjectileComponent>().Move(pos);
        StartCoroutine(AttackTimer());
    }
}
