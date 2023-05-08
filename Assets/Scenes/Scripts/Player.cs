using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rd;
    SpriteRenderer sprite;
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        speed = 5.0f;
    }
    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate() //�������� ������
    {
        //DeltaTime = Update / fixedDeltaTime = FixedUpdate
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rd.MovePosition(rd.position + nextVec);
    }
    //LateUpdate = ������ ���� ���� �����
    private void LateUpdate()
    {
        if (inputVec.x != 0)
        {
            sprite.flipX = inputVec.x < 0;
        }
    }
}