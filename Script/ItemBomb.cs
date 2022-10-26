using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBomb : MonoBehaviour
{
    [SerializeField]
    GameObject bombFx;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (collision.GetComponent<HeroMoveControlloer>().HEAD)
            {
                GameObject bomb = Instantiate(bombFx, transform.position, Quaternion.identity);
                Destroy(bomb , 0.5f); 
                Destroy(gameObject);
            }
        }
    }
}
