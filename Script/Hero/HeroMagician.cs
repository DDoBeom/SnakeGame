using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMagician : HeroComponent
{
    float offset = 2;

    public override void Attack(Vector3 pos)
    {
        isAttack = true;

        int dir = -1;

        for(int i = 0; i < 3; i++)
        {
            GameObject wea = Instantiate(weapon, transform.position, Quaternion.identity);
            wea.GetComponent<ProjectileComponent>().Move(new Vector3(pos.x + dir, pos.y, pos.z));
            dir++;

            Destroy(wea, 3);
        }

        StartCoroutine(AttackTimer());
    }
}
