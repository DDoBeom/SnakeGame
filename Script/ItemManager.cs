using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager im;

    [SerializeField]
    GameObject[] heroItems;                          //아이템 히어로를 담아둘 배열
    [SerializeField]
    GameObject[] gemsItem;                           //젬을 담아둘 배열\
    [SerializeField]
    GameObject hpItem;
    [SerializeField]
    GameObject bombItem;
    [SerializeField]
    GameObject iceItem;


    bool canSpawnHero = true;                   //현재 히어로 아이템을 스폰할 수 있는 지 판단할 변수
    List<bool> spawnHero = new List<bool>();    //현재 어떤 히어로들이 스폰되어있는지 아닌지 저장할 리스트
    int heroCount = 1;                          //현재 히어로 수
    AudioSource clip;

    // Start is called before the first frame update
    void Start()
    {
        im = this;

        for (int i = 0; i < 9; i++) spawnHero.Add(false);

        clip = GetComponent<AudioSource>();

        spawnHero[DataManager.dm.CURRENTHERO] = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem(Vector2 pos)
    {
        int temp = Random.Range(0, 5);

        if(temp == 0)
        {
            if (heroCount == 9) return;

            int num = Random.Range(0, 9);

            while (true)
            {
                if (spawnHero[num]) num = Random.Range(0, 9);           //랜덤한 히어로 아이템 재설정
                else
                {
                    spawnHero[num] = true;
                    break;
                }
            }

            GameObject hero = Instantiate(heroItems[num], pos, Quaternion.identity);            //히어로 아이템 생성

            heroCount++;            //히어로 수 증가
        }
        else if(temp == 1)
        {
            int num = Random.Range(0, 3);
            GameObject gem = Instantiate(gemsItem[num], pos, Quaternion.identity);
        }
        else if (temp == 2)
        {
            GameObject potion = Instantiate(hpItem, pos, Quaternion.identity);
        }
        else if(temp == 3)
        {
            GameObject bomb = Instantiate(bombItem, pos, Quaternion.identity);
        }
        else if (temp == 4)
        {
            GameObject ice = Instantiate(iceItem, pos, Quaternion.identity);
        }
    }

    public void AddHeroItem(int id)         //히어로 아이템 재추가
    {
        spawnHero[id] = false;
        heroCount--;
    }

    public void GetItemSound()
    {
        clip.Play();
    }
}
