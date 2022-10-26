using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour
{
    public static HeroManager hm;
    [SerializeField]
    GameObject[] prefabHeroes;          //영웅 생성 아이템 먹으면 생성될 영웅들
    [SerializeField]
    GameObject hpBar;                   //영웅 생성시 함께 생성될 Hp바
    GameObject hpBarPat;                //생성된 Hp바들의 부모 오브젝트

    [SerializeField]
    float inputRate = 0.1f;             //이동 키 입력 간격
    float inputTime = 0;                //간격 체크할 변수
    [SerializeField]
    float offset = 1.5f;                //영웅 간 간격

    bool isCanInput = false;            //현재 이동 키 입력 가능 불가능 체크할 변수

    int heroLength = 1;                 //현재 히어로 수
    [SerializeField]
    HeroMoveControlloer headHero;       //머리 히어로
    public Transform HEAD { get { return headHero.transform; } }            //머리 히어로 위치를 넘겨줄 변수
    [SerializeField]
    List<HeroMoveControlloer> heroList = new List<HeroMoveControlloer>();                   //히어로들을 담아둘 리스트

    public List<HeroMoveControlloer> LIST { get { return heroList; } }

    // Start is called before the first frame update
    void Start()
    {
        hm = this;
        hpBarPat = GameObject.Find("HeroHpBars");
        CreateHeadHero();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.GameStart || GameManager.gm.GameOver) return;

            if (!isCanInput)
        {
            inputTime += Time.deltaTime;
            if(inputTime>= inputRate)
            {
                isCanInput = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (headHero.DIR == Direction.UP || headHero.DIR == Direction.DOWN) return;

                ChangeMoveDirection(Direction.UP);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (headHero.DIR == Direction.UP || headHero.DIR == Direction.DOWN) return;

                ChangeMoveDirection(Direction.DOWN);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (headHero.DIR == Direction.LEFT || headHero.DIR == Direction.RIGHT) return;

                ChangeMoveDirection(Direction.LEFT);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (headHero.DIR == Direction.LEFT || headHero.DIR == Direction.RIGHT) return;

                ChangeMoveDirection(Direction.RIGHT);
            }
        }
    }

    void ChangeMoveDirection(Direction _dir)            //방향이동 함수
    {
        headHero.Move(_dir);

        if(heroList.Count > 1)
        {
            for(int i =1; i<heroList.Count; i++)
            {
                heroList[i].AddPosDir(headHero.transform.position, _dir);            //위치와 방향 저장
            }
        }

        inputTime = 0;
        isCanInput = false;
    }

    void CreateHeadHero()
    {
        GameObject hero = Instantiate(prefabHeroes[DataManager.dm.CURRENTHERO], transform.position, Quaternion.identity);
        hero.transform.SetParent(transform.GetChild(0));

        headHero = hero.GetComponent<HeroMoveControlloer>();
        heroList.Add(headHero);

        headHero.HEAD = true;
        headHero.Move(Direction.DOWN);

        CreatHpBar(headHero.GetComponentInChildren<HeroComponent>());
        GameManager.gm.GameBegin();
    }
    
    public void AddHero(int id)
    {
        HeroMoveControlloer preHero = heroList[heroLength - 1];                             //생성될 영웅 앞의 영웅을 가져옴

        Vector2 pos = Vector2.down;
        Vector2 pPos = preHero.transform.position;

        if (preHero.DIR == Direction.UP) pos = new Vector2(pPos.x, pPos.y - offset);                //위치 설정
        else if (preHero.DIR == Direction.DOWN) pos = new Vector2(pPos.x, pPos.y + offset);
        else if (preHero.DIR == Direction.LEFT) pos = new Vector2(pPos.x + offset, pPos.y);
        else pos = new Vector2(pPos.x - offset, pPos.y);

        GameObject temp = Instantiate(prefabHeroes[id], pos, Quaternion.identity);          //영웅 생성
        temp.transform.SetParent(transform.GetChild(0));                                    //생성한 영웅 부모 설정

        HeroMoveControlloer hero = temp.GetComponent<HeroMoveControlloer>();                //생성한 영웅의 히어로무브컨트롤러 담아둠

        hero.ID = heroLength;

        for(int i =0; i < preHero.GetPosDir().Count; i++)
        {
            hero.GetPosDir().Add(preHero.GetPosDir()[i]);           //앞 영웅의 위치와 방향이 저장된 리스트를 생성된 영웅에도 저장
        }

        CreatHpBar(hero.GetComponentInChildren<HeroComponent>());

        hero.Move(preHero.DIR);             //방향설정 및 이동 함수 호출!!

        heroLength++;                       //영웅 크기 추가
        heroList.Add(hero);                 //리스트에 영웅 추가
    }

    public void AllHeroDie()
    {
        foreach(HeroMoveControlloer i in heroList)
        {
            Destroy(i.gameObject);
        }

        GameManager.gm.GameEnd();
        heroList.Clear();
    }

    void CreatHpBar(HeroComponent hc)
    {
        GameObject bar = Instantiate(hpBar);                        //hp바 생성
        bar.transform.SetParent(hpBarPat.transform);                //hp바 부모설정
        hc.Init(50, 5, 0.5f, bar.GetComponent<Slider>());           //영웅에 hpBar 연결
    }

    public void RemoveHero(HeroMoveControlloer hmc)
    {
        for (int i = hmc.ID + 1; i < heroList.Count; i++) heroList[i].ID--;
        heroLength--;
        heroList.RemoveAt(hmc.ID);
    }

    public void GetHpItem(int _hp)
    {
        foreach (HeroMoveControlloer i in heroList) i.GetComponentInChildren<HeroComponent>().AddHp(_hp);
    }
}