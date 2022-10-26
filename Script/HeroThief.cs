using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroThief : HeroComponent
{
    public override void Attack(Vector3 pos)
    {
        isAttack = true;

        StartCoroutine(Yunsa(pos));
        StartCoroutine(AttackTimer());
    }

   IEnumerator Yunsa(Vector3 pos)
    {
        CreateWeapon(pos);
        yield return new WaitForSeconds(0.15f);
        CreateWeapon(pos);
        yield return new WaitForSeconds(0.15f);
        CreateWeapon(pos);
    }

    void CreateWeapon(Vector3 pos)
    {
        GameObject wep = Instantiate(weapon, transform.position, Quaternion.identity);
        wep.GetComponent<ProjectileComponent>().Move(pos);
    }
}
