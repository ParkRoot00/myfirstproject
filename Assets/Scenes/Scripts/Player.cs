using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public Scanner scanner;
    Rigidbody2D rd;
    SpriteRenderer sprite;
    Animator anim;
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>(); 
    }
    private void Start()
    {
        speed = 5.0f;
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate() //물리연산 프레임
    {
        //DeltaTime = Update / fixedDeltaTime = FixedUpdate
        if (!GameManager.instance.isLive)
            return;
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
    }
    //LateUpdate = 프레임 종료 직전 실행됨
    private void LateUpdate()
    {
        if (!GameManager.instance.isLive)
            return;

        anim.SetFloat("Speed",inputVec.magnitude);
        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.instance.isLive)
            return;
        GameManager.instance.health -= Time.deltaTime * 10;
        if (GameManager.instance.health <= 0)
        {
            
            for (int index = 2; index < transform.childCount; index++)
            {
                transform.GetChild(index).gameObject.SetActive(false);
            }
            GameManager.instance.GameOver();
            anim.SetTrigger("Dead");
            GameManager.instance.uiResult.SetActive(true);
        }
    }
    
}
