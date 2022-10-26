using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGems : MonoBehaviour
{
    [SerializeField]
    int gem = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (collision.GetComponent<HeroMoveControlloer>().HEAD)
            {
                //Áª Ãß°¡
                GameManager.gm.AddGems(gem);
                ItemManager.im.GetItemSound();
                Destroy(gameObject);
            }
        }
    }
}
