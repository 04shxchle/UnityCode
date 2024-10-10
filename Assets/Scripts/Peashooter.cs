using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peashooter : PlantBase
{
    public override float MaxHp
    {
        get
        {
            return 300;
        }
    }
    //是否可以攻击
    private bool canAttack = true;
    //攻击的CD，也就是攻击间隔
    private float attackCD = 1.4f;
    //攻击力
    private int attackValue=50;
    //创建子弹的偏移量
    private Vector3 createBulletOffsetPos = new Vector2(0.6f, 0.39f);

    protected override void OnInitForPlace()
    {
        //可能要攻击
        InvokeRepeating("Attack", 0, 0.2f);
    }

    /// <summary>
    /// 攻击方法—循环检测
    /// </summary>
    private void Attack()
    {
        if (canAttack == false) return;
        //从僵尸管理器中获取一个离我最近的僵尸
        Zombie zombie = ZombieManager.Instance.GetZombieByLineMinDistance((int)currGrid.Point.y, transform.position);
        //没有僵尸，跳出
        if (zombie == null) return;
        //僵尸必须在草坪上，否则跳出
        if (zombie.CurrGrid.Point.x == 8 && Vector2.Distance(zombie.transform.position, zombie.CurrGrid.Position) > 1.5f) return; 

        //从这里开始，可以正常攻击
        //从枪口实例化一个子弹
        Bullet bullet = GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Bullet1, transform.position +createBulletOffsetPos,Quaternion.identity,transform).GetComponent<Bullet>();
        bullet.Init(attackValue);
        CDEnter();
        canAttack= false;
}


    /// <summary>
    /// 进入CD
    /// </summary>
    private void CDEnter()
    {
        StartCoroutine(CalCD());
    }
    /// <summary>
    /// 计算冷却时间
    /// </summary>
    IEnumerator CalCD()
    {
        
            yield return new WaitForSeconds(attackCD);
            canAttack = true;

    }



}

