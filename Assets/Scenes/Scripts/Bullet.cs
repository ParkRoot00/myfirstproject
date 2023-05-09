using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rd;
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        BulletDead();
    }
    //초기화 함수
    public void Init(float damage, int per, Vector3 dir)
    {
        this.damage = damage;
        this.per = per;

        if (per > -1)
        {
            rd.velocity = dir * 15f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1)
            return;

        per--;

        if (per == -1)
        {
            rd.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
    void BulletDead()
    {
        Transform target = GameManager.instance.player.transform;
        Vector3 targetPos = target.position;
        float dir = Vector3.Distance(targetPos, transform.position);
        if (dir > 30f)
            this.gameObject.SetActive(false);
    }
}
