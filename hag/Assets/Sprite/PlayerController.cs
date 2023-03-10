using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�����̴� �ӵ�
    private float Speed;

    //�������� �����ϴ� ����
    private Vector3 Movemont;

    //�÷��̾��� Animator ������Ҹ� �޾ƿ������� ����
    public Animator animator;

    //�÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������
    public SpriteRenderer playerRenderer;

    //���� üũ
    private bool onAttack;
    private bool onHit;
    private bool onRoll;
    private bool onJumpU;

    //������ ����
    public GameObject BulletPrefad;

    //������ FX ����
    public GameObject fxPrefab;

    public GameObject[] stageBack = new GameObject[7];

    //������ �Ѿ��� �������
    private List<GameObject> Bullets = new List<GameObject>();

    //�÷��̾ ���������� �ٶ� ����
    private float Direction;

    //���̤����̾ �ٶ󺸴� ����
    public bool DirLeft;
    public bool DirRight;

    private void Awake()
    {
        //player�� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        //player�� SpriteRenderer�� �޾ƿ�.
        playerRenderer = this.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        //�ӵ��� �ʱ�ȭ(5.0f)
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
        //GetAxis = -1 ~ +1 ������ ���� ��ȯ
        //GetAxisRaw = -1 or 0 or 1 �� ������ ��ȯ
        float Hor = Input.GetAxisRaw("Horizontal");
        
        //Hor �� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�.
        if (Hor != 0)
            Direction = Hor;
        else
        {
            DirLeft = false;
            DirRight = false;
        }

        //�÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� �ø� ����.
        if (Direction < 0)
        {
            // playerRenderer.flipX = DirLeft = true;
            playerRenderer.flipX = DirLeft = true;
            //���� �÷��̾ �����δ�.
            transform.position += Movemont;
        }
        else if (Direction > 0)
        {
            playerRenderer.flipX = DirRight = false;
            DirRight = true;
        }



        //�Է¹��� ������ �÷��̾ ������
        Movemont = new Vector3(
        Hor * Time.deltaTime * Speed,
        0.0f,
        0.0f);

        //���� ��Ʈ��Ű�� �Է��Ѵٸ�
        if (Input.GetKey(KeyCode.LeftControl))
            OnAttack();//����
        if (Input.GetKey(KeyCode.LeftShift))
            OnHit();
        if (Input.GetKey(KeyCode.R))
            OnRoll();
        if (Input.GetKey(KeyCode.Space))
            OnJumpU();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //�Ѿ˿����� �����Ѵ�.
            GameObject Obj = Instantiate(BulletPrefad);
            //������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            Obj.transform.position = transform.position;
            //�Ѿ��� BullerController ��ũ��Ʈ�� �޾ƿ´�
            BulletController Controller = Obj.AddComponent<BulletController>();
            //�Ѿ� ��ũ��Ʈ ������ ���� ������ ���� �÷��̾��� ���� ������ �����Ѵ�.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            //�Ѿ� ��ũ��Ʈ ������ FX Prefab�� ����
            Controller.fxPrefab = fxPrefab;

            //�Ѿ��� SpriteRenderer�� �޾ƿ´�.
            SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();
            //�Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
            renderer.flipX = playerRenderer.flipX;
            //��� ������ ���� �Ǿ��ٸ� ����ҿ� �����Ѵ�.(������)
            Bullets.Add(Obj);
        }
    
        //�÷��̾��� �����ӿ� ���� �̵������ ����Ѵ�.
        animator.SetFloat("Speed", Hor);
    }

    void OnAttack()
    {
        //�̹� ���ݸ���� �������̶��
        if (onAttack)
            return;//�Լ��� ���� ��Ų��.

        //�Լ��� ������� �ʾҴٸ� ���ݻ��¸� Ȱ��ȭ �ϰ�
        onAttack = true;
        //���ݸ���� �����Ѵ�
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
        //�Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
        //�Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
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