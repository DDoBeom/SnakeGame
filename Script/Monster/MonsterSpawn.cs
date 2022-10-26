using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField]
    float minX = 0;
    [SerializeField]
    float maxX = 0;
    [SerializeField]
    float minY = 0;
    [SerializeField]
    float maxY = 0;
    
    [SerializeField]
    float spawnRate = 5;                //몬스터 생성 간격
    float time = 0;


    [SerializeField]
    GameObject monsterGroupPrefab;                  
    [SerializeField]
    GameObject[] monsters;              //생성될 몬스터들 담아둘 배열

    [SerializeField]
    GameObject hpBar;
    Transform hpBarParents;             //생성된 Hp바를 담아둘 게임 오브젝트

    [SerializeField]
    float offset = 1.5f;                //몬스터 생성시 앞 몬스터와의 간격

    // Start is called before the first frame update
    void Start()
    {
        hpBarParents = GameObject.Find("MonsterHpBars").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.GameStart || GameManager.gm.GameOver) return;

        time += Time.deltaTime;

        if (time >= spawnRate) SpawnMonster();
    }

    void SpawnMonster()
    {
        Direction dir = (Direction)Random.Range(0, 4);              //방향 설정

        Vector2 pos = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));                   //위치 설정

        GameObject group = Instantiate(monsterGroupPrefab);         //몬스터 그룹 생성
        group.transform.SetParent(this.transform);                  //몬스터 그룹 부모 설정

        int monLangth = Random.Range(1, 4);                         //생성할 몬스터 갯수 설정

        for (int i = 0; i < monLangth; i++)                         //몬스터 생성
        {
            GameObject _hpBar = Instantiate(hpBar);                 //hp바 생성
            _hpBar.transform.SetParent(hpBarParents);               //hp바 부모 설정

            GameObject mon = Instantiate(monsters[Random.Range(0, monsters.Length)]);                   //몬스터 생성
            mon.transform.SetParent(group.transform);               //몬스터 부모 설정
            mon.GetComponent<MonsterMoveController>().ID = i;       //몬스터 인덱스 설정
            mon.GetComponent<MonsterComponent>().hpBar = _hpBar.GetComponent<Slider>();                 //몬스터와 hp바 연결
            mon.GetComponent<MonsterComponent>().SetHpbar();

            if (i == 0)
            {
                mon.transform.position = pos;                       //몬스터 위치 설정
                mon.GetComponent<MonsterMoveController>().HEAD = true;
                group.GetComponent<MonsterControl>().SetHeadMonster(mon.GetComponent<MonsterMoveController>());
                group.GetComponent<MonsterControl>().GetHeadMonster().Move(dir);
            }
            else
            {
                MonsterMoveController preMonster = group.GetComponent<MonsterControl>().GetList(i - 1); //생성된 몬스터 앞에 있는 몬스터를 가져옴

                FollowDirection(preMonster, mon);                   //위치 설정

                for(int j=0; j < preMonster.GetPosDir().Count; j++)
                {
                    mon.GetComponent<MonsterMoveController>().AddPosDir(preMonster.GetPosDir()[j].pos, preMonster.GetPosDir()[j].dir);  
                    //앞 몬스터에 저장되어 있는 방향 변경 데이터를 생성한 몬스터 리스트에 담아줌
                }

                mon.GetComponent<MonsterMoveController>().Move(preMonster.DIR);
            }

            group.GetComponent<MonsterControl>().AddMonster(mon.GetComponent<MonsterMoveController>());             //그룹 몬스터 리스트에 추가
        }

        time = 0;
    }

    void FollowDirection(MonsterMoveController preMon, GameObject mon)
    {
        Vector2 prePos = preMon.transform.position;

        if (preMon.DIR == Direction.UP) mon.transform.position = new Vector2(preMon.transform.position.x, prePos.y - offset);
        else if (preMon.DIR == Direction.DOWN) mon.transform.position = new Vector2(preMon.transform.position.x, prePos.y + offset);
        else if (preMon.DIR == Direction.LEFT) mon.transform.position = new Vector2(preMon.transform.position.x + offset, prePos.y);
        else mon.transform.position = new Vector2(prePos.x - offset, prePos.y);
    }
}
