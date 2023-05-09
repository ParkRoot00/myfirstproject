using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rd;
    Animator anim;
    SpriteRenderer sprite;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!isLive)
            return;
        Vector2 dirVec = target.position - rd.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
        rd.velocity = Vector2.zero;
    }
    private void LateUpdate()
    {
        if (!isLive)
            return;
        sprite.flipX = target.position.x < rd.position.x;
    }
    private void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    //초기 속성 적용 함수
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) //지금 충돌한 Tag 체크
        {
            return;
        }
        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            //Live
        }
        else
        {
            //Die
            Dead();
        }
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
