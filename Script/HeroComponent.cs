using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroComponent : MonoBehaviour
{
    protected int heroId = 0;
    
    [SerializeField]
    int hp = 50;
    [SerializeField]
    int atk = 5;
    [SerializeField]
    float atkRate = 0.5f;

    protected bool isAttack = false;

    Slider hpBar;

    [SerializeField]
    protected GameObject weapon;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = transform.parent.GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (hpBar) hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + new Vector3(0, 1.5f, 0));
    }



    public void Init(int _hp, int _atk, float _rate, Slider slider)               //히어로 초기화 함수
    {
        hp = _hp;
        atk = _atk;
        //atkRate = _rate;
        hpBar = slider;
        hpBar.maxValue = hp;
        hpBar.value = hp;
    }

    public virtual void Attack(Vector3 pos)
    {

    }

    protected IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(atkRate);
        isAttack = false;
    }

    public void TakeDamage(int dmg)
    {
        if (hp <= 0) return;
        hp -= dmg;
        StartCoroutine(HitColorChange());
        hpBar.value = hp;

        if (hp <= 0) Die();
    }

    public void Die()
    {
        HeroManager.hm.RemoveHero(GetComponentInParent<HeroMoveControlloer>());

        Destroy(hpBar.gameObject);
        Destroy(transform.parent.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Monster")&& !isAttack)
        {
            Attack(collision.transform.transform.position);
        }
    }

    IEnumerator HitColorChange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    public void AddHp(int _hp)
    {
        hp += _hp;

        if (hpBar.maxValue < hp) hp = (int)hpBar.maxValue;

        hpBar.value = hp;
    }
}
