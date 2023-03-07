using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //움직이는 속도
    private float Speed;
    private Vector3 Movemont;
    public Animator animator;

    private bool onAttack;
    private bool onHit;
    private bool onRoll;
    private bool onJumpU;

    // Start is called before the first frame update
    void Start()
    {
        //속도를 초기화(5.0f)
        Speed = 5.0f;

        //player의 Animator를 받아온다.
        animator = this.GetComponent<Animator>();

        onAttack = false;
        onHit = false;
        onRoll = false;
        onJumpU = false;
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis = -1 ~ +1 사이의 값을 반환
        //GetAxisRaw = -1 or 0 or 1 의 값만을 반환
        float Hor = Input.GetAxis("Horizontal");
        float Ver = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        if (Input.GetKey(KeyCode.R))
            OnRoll();
        if (Input.GetKey(KeyCode.Space))
            OnJumpU();

        Movemont = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed,
            0.0f);

        animator.SetFloat("Speed", Hor);
        transform.position += Movemont;

        animator.SetFloat("Rope", Ver);
        transform.position += Movemont;
    }

    void OnAttack()
    {
        if (onAttack)
            return;

        onAttack = true;
        animator.SetTrigger("Attack");
    }
    void OnHit()
    {
        if (onHit)
            return;

        onHit = true;
        animator.SetTrigger("Hit");
    }
    void OnRoll()
    {
        if (onRoll)
            return;

        onRoll = true;
        animator.SetTrigger("Roll");
    }

    void OnJumpU()
    {
        if (onJumpU)
            return;

        onJumpU = true;
        animator.SetTrigger("JumpU");
    }

    private void SetAttack()
    {
        onAttack = false;
    }
    private void SetHit()
    {
        onHit = false;
    }
    private void SetRoll()
    {
        onRoll = false;
    }

    private void SetJumpU()
    {
        onJumpU = false;
    }

}