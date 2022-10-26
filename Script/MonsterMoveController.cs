using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMoveController : HeroMoveControlloer
{
    bool isIce = false;
    public bool ICE { get { return isIce; } }
    [SerializeField]
    GameObject iceFx;
    float iceTime = 4;
    Coroutine cor;
    float preSpeed = 0;                     //현재 스피드 임시 저장할 변수

    public void Icing()
    {
        if (isIce)
        {
            Destroy(transform.GetChild(2).gameObject); //기존의 IceFx 삭제
            StopCoroutine(cor);
        }
        else preSpeed = speed;              //현재 스피드 임시 저장

        cor = StartCoroutine(Ice());
    }

    IEnumerator Ice()
    {
        isIce = true;
        anit.speed = 0;                     //애니메이션 스톱
        speed = 0;
        SetSpeed();

        
        GameObject temp = Instantiate(iceFx, transform.position, Quaternion.identity);
        temp.transform.SetParent(transform);

        yield return new WaitForSeconds(iceTime);

        speed = preSpeed;                  //원래 스피드
        anit.speed = 1;
        Destroy(temp);
        SetSpeed();

        
        isIce = false;
    }
}
