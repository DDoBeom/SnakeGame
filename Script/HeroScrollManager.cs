using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HeroScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    const int SIZE = 9;

    Scrollbar sBar;

    float[] pos = new float[SIZE];                  //각 히어로들의 위치가 저장될 배열
    float distance;                                 //각 히어로들간의 간격
    float targetPos;

    bool isDrag = false;

    GameObject hero;
    AudioSource clip;


    // Start is called before the first frame update
    void Start()
    {
        sBar = GetComponentInChildren<Scrollbar>();
        clip = GetComponent<AudioSource>();
        distance = 1f / (SIZE - 1);                 //영웅들의 간격 구하기

        for(int i = 0; i < SIZE; i++)
        {
            pos[i] = distance * i;                  //각 히어로들의 위치 배열에 저장
        }

        hero = transform.GetChild(0).GetChild(0).gameObject;
        hero.transform.GetChild(0).GetComponent<Animator>().SetBool("isSelect", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDrag) sBar.value = Mathf.Lerp(sBar.value, targetPos, 0.1f);          //현재 위치에서 타겟위치로 이동
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        for (int i = 0; i < SIZE; i++)
        {
            if(sBar.value < pos[i] + distance *0.5f && sBar.value > pos[i] - distance * 0.5f)
            {
                targetPos = pos[i];
                DataManager.dm.ChangeStartHero(i);
                clip.Play();
                SetAnimator(i);

            }
        }
    }

    void SetAnimator(int idx)
    {
        for(int i = 0; i < hero.transform.childCount; i++)
        {
            if (i == idx) hero.transform.GetChild(i).GetComponent<Animator>().SetBool("isSelect", true);
            else hero.transform.GetChild(i).GetComponent<Animator>().SetBool("isSelect", false);
        }
    }
}
