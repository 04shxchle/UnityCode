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
    //�Ƿ���Թ���
    private bool canAttack = true;
    //������CD��Ҳ���ǹ������
    private float attackCD = 1.4f;
    //������
    private int attackValue=50;
    //�����ӵ���ƫ����
    private Vector3 createBulletOffsetPos = new Vector2(0.6f, 0.39f);

    protected override void OnInitForPlace()
    {
        //����Ҫ����
        InvokeRepeating("Attack", 0, 0.2f);
    }

    /// <summary>
    /// ����������ѭ�����
    /// </summary>
    private void Attack()
    {
        if (canAttack == false) return;
        //�ӽ�ʬ�������л�ȡһ����������Ľ�ʬ
        Zombie zombie = ZombieManager.Instance.GetZombieByLineMinDistance((int)currGrid.Point.y, transform.position);
        //û�н�ʬ������
        if (zombie == null) return;
        //��ʬ�����ڲ�ƺ�ϣ���������
        if (zombie.CurrGrid.Point.x == 8 && Vector2.Distance(zombie.transform.position, zombie.CurrGrid.Position) > 1.5f) return; 

        //�����￪ʼ��������������
        //��ǹ��ʵ����һ���ӵ�
        Bullet bullet = GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Bullet1, transform.position +createBulletOffsetPos,Quaternion.identity,transform).GetComponent<Bullet>();
        bullet.Init(attackValue);
        CDEnter();
        canAttack= false;
}


    /// <summary>
    /// ����CD
    /// </summary>
    private void CDEnter()
    {
        StartCoroutine(CalCD());
    }
    /// <summary>
    /// ������ȴʱ��
    /// </summary>
    IEnumerator CalCD()
    {
        
            yield return new WaitForSeconds(attackCD);
            canAttack = true;

    }



}

