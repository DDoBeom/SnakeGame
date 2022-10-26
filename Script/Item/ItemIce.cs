using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIce : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (collision.GetComponent<HeroMoveControlloer>().HEAD)
            {
                GameObject monsters = GameObject.Find("MonsterSpawner");

                for(int i = 0; i < monsters.transform.childCount; i++)
                {
                    monsters.transform.GetChild(i).GetComponent<MonsterControl>().MonsterIce();
                }

                Destroy(gameObject);
            }
        }
    }
}
