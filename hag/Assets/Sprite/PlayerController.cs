using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //움직이는 속도
    private float Speed;

    //움직임을 저장하는 벡터
    private Vector3 Movemont;

    //플레이어의 Animator 구성요소를 받아오기위해 만듬
    public Animator animator;

    //플레이어의 SpriteRenderer 구성요소를 받아오기위함
    public SpriteRenderer playerRenderer;

    //상태 체크
    private bool onAttack;
    private bool onHit;
    private bool onRoll;
    private bool onJumpU;

    //복사할 원본
    public GameObject BulletPrefad;

    //복제할 FX 원본
    public GameObject fxPrefab;

    public GameObject[] stageBack = new GameObject[7];

    //복제된 총알의 저장공간
    private List<GameObject> Bullets = new List<GameObject>();

    //플레이어가 마지막으로 바라본 방향
    private float Direction;

    //프ㅜㄹ레이어가 바라보는 방향
    public bool DirLeft;
    public bool DirRight;

    private void Awake()
    {
        //player의 Animator를 받아온다.
        animator = this.GetComponent<Animator>();

        //player의 SpriteRenderer을 받아옴.
        playerRenderer = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //속도를 초기화(5.0f)
        Speed = 5.0f;

        onAttack = false;
        onHit = false;
        onRoll = false;
        onJumpU = false;

        Direction = 1.0f;

        for(int i=0;i<7; ++i)
        {
            stageBack[i] = GameObject.Find(i.ToString());
        }
    }
    void Update()
    {
        //GetAxis = -1 ~ +1 사이의 값을 반환
        //GetAxisRaw = -1 or 0 or 1 의 값만을 반환
        float Hor = Input.GetAxisRaw("Horizontal");
        
        //Hor 이 0이라면 멈춰있는 상태이므로 예외처리를 해준다.
        if (Hor != 0)
            Direction = Hor;
        else
        {
            DirLeft = false;
            DirRight = false;
        }

        //플레이어가 바라보고 있는 방향에 따라 이미지 플립 설정.
        if (Direction < 0)
        {
            // playerRenderer.flipX = DirLeft = true;
            playerRenderer.flipX = DirLeft = true;
            //실제 플레이어를 움직인다.
            transform.position += Movemont;
        }
        else if (Direction > 0)
        {
            playerRenderer.flipX = DirRight = false;
            DirRight = true;
        }



        //입력받은 값으로 플레이어를 움직임
        Movemont = new Vector3(
        Hor * Time.deltaTime * Speed,
        0.0f,
        0.0f);

        //좌측 컨트롤키를 입력한다면
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();//공격
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        if (Input.GetKey(KeyCode.R))
            OnRoll();
        if (Input.GetKey(KeyCode.Space))
            OnJumpU();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //총알원본을 복제한다.
            GameObject Obj = Instantiate(BulletPrefad);
            //복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
            Obj.transform.position = transform.position;
            //총알의 BullerController 스크립트를 받아온다
            BulletController Controller = Obj.AddComponent<BulletController>();
            //총알 스크립트 내부의 방향 변수를 현재 플레이어의 방향 변수로 설정한다.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            //총알 스크립트 내부의 FX Prefab을 설정
            Controller.fxPrefab = fxPrefab;

            //총알의 SpriteRenderer를 받아온다.
            SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();
            //총알의 이미지 반전 상태를 플레이어의 이미지 반전 상태로 설정한다.
            renderer.flipX = playerRenderer.flipX;
            //모든 설정이 종료 되었다면 저장소에 보관한다.(만들어서쏨)
            Bullets.Add(Obj);
        }
    
        //플레이어의 움직임에 따라 이동모션을 출력한다.
        animator.SetFloat("Speed", Hor);
    }

    void OnAttack()
    {
        //이미 공격모션이 진행중이라면
        if (onAttack)
            return;//함수를 종료 시킨다.

        //함수가 종료되지 않았다면 공격상태를 활성화 하고
        onAttack = true;
        //공격모션을 실행한다
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
        //함수가 실행되면 공격모션이 비활성화 된다.
        //함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
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