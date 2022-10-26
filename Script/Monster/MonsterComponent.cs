using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterComponent : MonoBehaviour
{
    [SerializeField]
    int hp = 10;
    [SerializeField]
    int atk = 5;
    [SerializeField]
    float attackRate = 0.5f;
    bool isAttack = false;

    public Slider hpBar;
    SpriteRenderer sr;

    AudioSource clip;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        clip = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (hpBar)
            hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 1.5f, 0));
    }

    public void TakeDamage(int dmg)
    {
        if (hp <= 0) return;

        hp -= dmg;
        StartCoroutine(HitColorChange());
        hpBar.value = hp;
        clip.Play();

        if (hp <= 0) Die();
    }

    public void Die()
    {
        GetComponentInParent<MonsterControl>().RemoveMonster(GetComponent<MonsterMoveController>());

        Destroy(hpBar.gameObject);
        Destroy(gameObject);
    }

    public void SetHpbar()
    {
        //hpBar = _hpBar.GetComponent<Slider>();                 //hp바 변수에 hp바 담아줌
        hpBar.maxValue = hp;            //hp바 맥스값 설정
        hpBar.value = hp;               //hp바 현재값 설정
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero") && !isAttack)
        {
            collision.GetComponentInChildren<HeroComponent>().TakeDamage(atk);
            StartCoroutine(AttackRate());
        }
    }

    IEnumerator HitColorChange()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    IEnumerator AttackRate()
    {
        isAttack = true;
        yield return new WaitForSeconds(attackRate);
        isAttack = false;
    }
}
