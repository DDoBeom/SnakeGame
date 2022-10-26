using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageRotationComponent : MonoBehaviour
{
    [SerializeField]
    float speed = 3;


    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + speed * Time.deltaTime);
    }
}
