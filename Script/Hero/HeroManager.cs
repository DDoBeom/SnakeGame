using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroManager : MonoBehaviour
{
    public static HeroManager hm;
    [SerializeField]
    GameObject[] prefabHeroes;          //���� ���� ������ ������ ������ ������
    [SerializeField]
    GameObject hpBar;                   //���� ������ �Բ� ������ Hp��
    GameObject hpBarPat;                //������ Hp�ٵ��� �θ� ������Ʈ

    [SerializeField]
    float inputRate = 0.1f;             //�̵� Ű �Է� ����
    float inputTime = 0;                //���� üũ�� ����
    [SerializeField]
    float offset = 1.5f;                //���� �� ����

    bool isCanInput = false;            //���� �̵� Ű �Է� ���� �Ұ��� üũ�� ����

    int heroLength = 1;                 //���� ����� ��
    [SerializeField]
    HeroMoveControlloer headHero;       //�Ӹ� �����
    public Transform HEAD { get { return headHero.transform; } }            //�Ӹ� ����� ��ġ�� �Ѱ��� ����
    [SerializeField]
    List<HeroMoveControlloer> heroList = new List<HeroMoveControlloer>();                   //����ε��� ��Ƶ� ����Ʈ

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

    void ChangeMoveDirection(Direction _dir)            //�����̵� �Լ�
    {
        headHero.Move(_dir);

        if(heroList.Count > 1)
        {
            for(int i =1; i<heroList.Count; i++)
            {
                heroList[i].AddPosDir(headHero.transform.position, _dir);            //��ġ�� ���� ����
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
        HeroMoveControlloer preHero = heroList[heroLength - 1];                             //������ ���� ���� ������ ������

        Vector2 pos = Vector2.down;
        Vector2 pPos = preHero.transform.position;

        if (preHero.DIR == Direction.UP) pos = new Vector2(pPos.x, pPos.y - offset);                //��ġ ����
        else if (preHero.DIR == Direction.DOWN) pos = new Vector2(pPos.x, pPos.y + offset);
        else if (preHero.DIR == Direction.LEFT) pos = new Vector2(pPos.x + offset, pPos.y);
        else pos = new Vector2(pPos.x - offset, pPos.y);

        GameObject temp = Instantiate(prefabHeroes[id], pos, Quaternion.identity);          //���� ����
        temp.transform.SetParent(transform.GetChild(0));                                    //������ ���� �θ� ����

        HeroMoveControlloer hero = temp.GetComponent<HeroMoveControlloer>();                //������ ������ ����ι�����Ʈ�ѷ� ��Ƶ�

        hero.ID = heroLength;

        for(int i =0; i < preHero.GetPosDir().Count; i++)
        {
            hero.GetPosDir().Add(preHero.GetPosDir()[i]);           //�� ������ ��ġ�� ������ ����� ����Ʈ�� ������ �������� ����
        }

        CreatHpBar(hero.GetComponentInChildren<HeroComponent>());

        hero.Move(preHero.DIR);             //���⼳�� �� �̵� �Լ� ȣ��!!

        heroLength++;                       //���� ũ�� �߰�
        heroList.Add(hero);                 //����Ʈ�� ���� �߰�
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
        GameObject bar = Instantiate(hpBar);                        //hp�� ����
        bar.transform.SetParent(hpBarPat.transform);                //hp�� �θ���
        hc.Init(50, 5, 0.5f, bar.GetComponent<Slider>());           //������ hpBar ����
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