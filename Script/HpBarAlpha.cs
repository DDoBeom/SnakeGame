using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarAlpha : MonoBehaviour
{
    Animation anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (!anim) anim = GetComponent<Animation>();
        anim.Play();
    }
}
