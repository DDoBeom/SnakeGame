using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPotion : MonoBehaviour
{
    [SerializeField]
    int hp = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            if (collision.GetComponent<HeroMoveControlloer>().HEAD)
            {
                //hp추가 함수 호출
                HeroManager.hm.GetHpItem(hp);
                ItemManager.im.GetItemSound();
                Destroy(gameObject);
            }
        }
    }
}
