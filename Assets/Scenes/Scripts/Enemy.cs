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
    Collider2D coll;
    Animator anim;
    SpriteRenderer sprite;
    WaitForFixedUpdate wait;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();
    }
    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
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
        coll.enabled = true;
        rd.simulated = true;
        sprite.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;
    }

    //�ʱ� �Ӽ� ���� �Լ�
    public void Init(SpawnData data)
    {
        anim.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")|| !isLive) //���� �浹�� Tag üũ
            return;
        health -= collision.GetComponent<Bullet>().damage;
        KnockBack();

        if (health > 0)
        {
            //Live
            anim.SetTrigger("Hit");
        }
        else
        {
            //Die
            isLive = false;
            coll.enabled = false;
            rd.simulated = false;
            sprite.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }
    }

    IEnumerable KnockBack()
    {
        /*
        yield return null; // 1������ ��
        yield return new WaitForSeconds(2f); //2�� ��
        */
        yield return wait; //���� �ϳ��� ���� ������ ������
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rd.AddForce(dirVec.normalized * 10f, ForceMode2D.Impulse);

    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}
