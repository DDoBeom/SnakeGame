using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    [SerializeField]
    int atk = 5;

    [SerializeField]
    float destroyTime = 0;
    [SerializeField]
    bool isDestroy = false;
    [SerializeField]
    GameObject deleteObj;
    [SerializeField]
    bool isIce = false;

    int iceChance = 5;

    private void Start()
    {
        DestroyObj(destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            collision.GetComponent<MonsterComponent>().TakeDamage(atk);

            if (isIce)
            {
                int ran = Random.Range(0, 100);

                if (iceChance > ran) collision.GetComponentInParent<MonsterControl>().MonsterIce();
            }

            if (isDestroy) DestroyObj(0);
        }
    }

    void DestroyObj(float t)
    {
        Destroy(deleteObj, t);  
    }
}
