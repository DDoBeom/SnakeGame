using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileComponent : MonoBehaviour
{
    [SerializeField]
    float speed = 10;
    Rigidbody2D rb;

    [SerializeField]
    bool isDestroy = false;

    bool hasTarget = false;
    Vector3 target;

    [SerializeField]
    GameObject bombFX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTarget)
        {
            if (Vector3.Distance(transform.position, target) <= 0.1f)
            {
                Instantiate(bombFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void Move(Vector3 pos)
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 dir = (pos - transform.position).normalized * speed;
        rb.velocity = dir;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void SetTarget(Vector3 pos)
    {
        target = pos;
        hasTarget = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDestroy) return;

        if (collision.CompareTag("Monster"))
        {
            Instantiate(bombFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
