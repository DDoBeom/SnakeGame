using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (!GameManager.gm.GameStart || GameManager.gm.GameOver) return;
        transform.position = HeroManager.hm.HEAD.position;
    }

}
