using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroBomb : HeroComponent
{

    public override void Attack(Vector3 pos)
    {
        isAttack = true;
        GameObject temp = Instantiate(weapon, transform.position, Quaternion.identity);
        ItemManager.im.GetItemSound();
        temp.GetComponent<ProjectileComponent>().SetTarget(pos);
        temp.GetComponent<ProjectileComponent>().Move(pos);

        StartCoroutine(AttackTimer());
    }
}
