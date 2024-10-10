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
    //僵尸的状态
    private ZombieState state;
    private Animator animator;
    private Grid currGrid;
    //僵尸生命值
    private int hp = 270;

    /// <summary>
    /// 速度，决定了我几秒走一格
    /// </summary>
    private float speed = 6;
    //在攻击中
    private bool isAttackState;

    //攻击力 每秒伤害
    private float attackValue=100;

    //行走动画的名称,随机来
    private string walkAnimationStr;
    //攻击动画的名称
    private string attackAnimationStr;


    //是否已经失去头
    private bool isLostHead=false;

    /// <summary>
    /// 修改状态会直接改变动画
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
                //头需要失去
                isLostHead = true;
                walkAnimationStr = "Zombie_LostHead";
                attackAnimationStr = "Zombie_LostHeadAttack";
                //创建一个头
                GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Zombie_Head, animator.transform.position, Quaternion.identity, null);
                //状态检测
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
            Debug.LogError("ZombieManager未正确初始化");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FSM();
    }

    

    /// <summary>
    /// 状态检测
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case ZombieState.Idel:
                //播放行走画面，但是要卡在第一帧
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
    /// 状态检测
    /// </summary>
    private void FSM()
    {
        switch (State)
        {
            case ZombieState.Idel:
                state = ZombieState.Walk;
                break;
            case ZombieState.Walk:
                //一直向左走，并且遇到植物会攻击，攻击结束继续走
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
    /// 获取一个网格，决定我在第几排出现
    /// </summary>
    /// <param name="vertivalNum"></param>
    private void GetGridByVerticalNum(int vertivalNum)
    {
        currGrid = GridManager.Instance.GetGridByVerticalNum(vertivalNum);
        transform.position = new Vector3(transform.position.x, CurrGrid.Position.y);

    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Move()
    {
        //如果当前网格中为空跳过移动检测
        if (CurrGrid == null)
        {
            return;
        }
        currGrid= GridManager.Instance.GetGridByWorldPos(transform.position);
        //当前网格中有植物并且植物在僵尸左边并且距离很近
        if (CurrGrid.HavePlant
            &&CurrGrid.CurrPlantBase.transform.position.x <transform.position.x
            &&transform.position.x-CurrGrid.CurrPlantBase.transform.position.x<0.3f)
        {
            //攻击植物
            state=ZombieState.Attack;
            
            return;
        }

        transform.Translate((new Vector2(-1.33f, 0) * (Time.deltaTime / 1)) / speed);
    }

    private void Attack(PlantBase plant)
    {
        isAttackState = true;
        //植物的相关逻辑
        StartCoroutine(DoHurtPlant(plant));
        //animator.Play("Zombie_Attack");
    }

    /// <summary>
    /// 附加伤害给植物
    /// </summary>
    /// <returns></returns>
    IEnumerator DoHurtPlant(PlantBase plant)
    {
        //植物的生命大于0则扣血
        while(plant.Hp>0)
        {
            plant.Hurt(attackValue/5);
            yield return new WaitForSeconds(0.2f);
        }
        isAttackState= false;
        State = ZombieState.Walk;
    }

    /// <summary>
    /// 僵尸自身受伤
    /// </summary>
    public void Hurt(int attackValue)
    {
        Hp -= attackValue;
    }


    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        //告诉僵尸管理器我死了
        ZombieManager.Instance.RemoveZombie(this);
        Destroy(gameObject);
    }
}
