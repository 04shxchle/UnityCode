using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum ZombieState
{
    Idel,
    Walk,
    Attack,
    Dead
}

public class Zombie : MonoBehaviour
{
    //��ʬ��״̬
    private ZombieState state;
    private Animator animator;
    private Grid currGrid;
    //��ʬ����ֵ
    private int hp = 270;

    /// <summary>
    /// �ٶȣ��������Ҽ�����һ��
    /// </summary>
    private float speed = 6;
    //�ڹ�����
    private bool isAttackState;

    //������ ÿ���˺�
    private float attackValue=100;

    //���߶���������,�����
    private string walkAnimationStr;
    //��������������
    private string attackAnimationStr;


    //�Ƿ��Ѿ�ʧȥͷ
    private bool isLostHead=false;

    /// <summary>
    /// �޸�״̬��ֱ�Ӹı䶯��
    /// </summary>
    public ZombieState State { get => state;
        set
        {
            state = value;
            CheckState();
        }
    }

    public Grid CurrGrid { get => currGrid;  }
    public int Hp { get => hp; set
        {
            hp = value;
            if(hp<=90&&isLostHead)
            {
                //ͷ��Ҫʧȥ
                isLostHead = true;
                walkAnimationStr = "Zombie_LostHead";
                attackAnimationStr = "Zombie_LostHeadAttack";
                //����һ��ͷ
                GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Zombie_Head, animator.transform.position, Quaternion.identity, null);
                //״̬���
                CheckState();
            }
            if(hp<=0)
            {
                State = ZombieState.Dead;
            }
        }
    }

    private void Awake()
    {
        int rangeWalk = Random.Range(1, 4);
        switch (rangeWalk)
        {
            case 1:
                walkAnimationStr = "Zombie_Walk1";
                break;
            case 2:
                walkAnimationStr = "Zombie_Walk1 1";
                break;
            case 3:
                walkAnimationStr = "Zombie_Walk1 2";
                break;
        }
        //animator.Play(walkAnimationStr);
        attackAnimationStr = "Zombie_Attack";
        GetGridByVerticalNum(0);
    }
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        if (ZombieManager.Instance != null)
        {
            ZombieManager.Instance.AddZombie(this);
        }
        else
        {
            Debug.LogError("ZombieManagerδ��ȷ��ʼ��");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
    }

    

    /// <summary>
    /// ״̬���
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case ZombieState.Idel:
                //�������߻��棬����Ҫ���ڵ�һ֡
                animator.Play(walkAnimationStr, 0, 0);
                animator.speed = 0;
                break;
            case ZombieState.Walk:
                animator.Play(walkAnimationStr,0,animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                animator.speed = 1;
                break;
            case ZombieState.Attack:
                animator.Play(attackAnimationStr, 0, animator.GetCurrentAnimatorStateInfo(0).normalizedTime);

                animator.speed = 1;
                break;
            case ZombieState.Dead:
                break;
        }
    }


    /// <summary>
    /// ״̬���
    /// </summary>
    private void FSM()
    {
        switch (State)
        {
            case ZombieState.Idel:
                state = ZombieState.Walk;
                break;
            case ZombieState.Walk:
                //һֱ�����ߣ���������ֲ��ṥ������������������
                Move();
                break;
            case ZombieState.Attack:
                if (isAttackState) break;
                Attack(CurrGrid.CurrPlantBase);
                break;
            case ZombieState.Dead:
                Dead();
                break;
        }
    }

    /// <summary>
    /// ��ȡһ�����񣬾������ڵڼ��ų���
    /// </summary>
    /// <param name="vertivalNum"></param>
    private void GetGridByVerticalNum(int vertivalNum)
    {
        currGrid = GridManager.Instance.GetGridByVerticalNum(vertivalNum);
        transform.position = new Vector3(transform.position.x, CurrGrid.Position.y);

    }

    /// <summary>
    /// �ƶ�
    /// </summary>
    private void Move()
    {
        //�����ǰ������Ϊ�������ƶ����
        if (CurrGrid == null)
        {
            return;
        }
        currGrid= GridManager.Instance.GetGridByWorldPos(transform.position);
        //��ǰ��������ֲ�ﲢ��ֲ���ڽ�ʬ��߲��Ҿ���ܽ�
        if (CurrGrid.HavePlant
            &&CurrGrid.CurrPlantBase.transform.position.x <transform.position.x
            &&transform.position.x-CurrGrid.CurrPlantBase.transform.position.x<0.3f)
        {
            //����ֲ��
            state=ZombieState.Attack;
            
            return;
        }

        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1)) / speed);
    }

    private void Attack(PlantBase plant)
    {
        isAttackState = true;
        //ֲ�������߼�
        StartCoroutine(DoHurtPlant(plant));
        //animator.Play("Zombie_Attack");
    }

    /// <summary>
    /// �����˺���ֲ��
    /// </summary>
    /// <returns></returns>
    IEnumerator DoHurtPlant(PlantBase plant)
    {
        //ֲ�����������0���Ѫ
        while(plant.Hp>0)
        {
            plant.Hurt(attackValue/5);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState= false;
        State = ZombieState.Walk;
    }

    /// <summary>
    /// ��ʬ��������
    /// </summary>
    public void Hurt(int attackValue)
    {
        Hp -= attackValue;
    }


    /// <summary>
    /// ����
    /// </summary>
    private void Dead()
    {
        //���߽�ʬ������������
        ZombieManager.Instance.RemoveZombie(this);
        Destroy(gameObject);
    }
}
