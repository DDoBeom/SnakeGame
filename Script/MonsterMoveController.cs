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
    float preSpeed = 0;                     //���� ���ǵ� �ӽ� ������ ����

    public void Icing()
    {
        if (isIce)
        {
            Destroy(transform.GetChild(2).gameObject); //������ IceFx ����
            StopCoroutine(cor);
        }
        else preSpeed = speed;              //���� ���ǵ� �ӽ� ����

        cor = StartCoroutine(Ice());
    }

    IEnumerator Ice()
    {
        isIce = true;
        anit.speed = 0;                     //�ִϸ��̼� ����
        speed = 0;
        SetSpeed();

        
        GameObject temp = Instantiate(iceFx, transform.position, Quaternion.identity);
        temp.transform.SetParent(transform);

        yield return new WaitForSeconds(iceTime);

        speed = preSpeed;                  //���� ���ǵ�
        anit.speed = 1;
        Destroy(temp);
        SetSpeed();

        
        isIce = false;
    }
}