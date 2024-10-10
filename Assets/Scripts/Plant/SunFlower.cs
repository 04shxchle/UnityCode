using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : PlantBase
{
   
    //����������Ҫ��ʱ��
    private float createSunTime=24;
    //��ɽ�ɫ��Ҫ��ʱ��
    private float goldWantTime=1;

    public override float MaxHp
    {
        get
        {
            return 300;
        }
    }

    protected override void OnInitForPlace()
    {
        hp = 300f;
        InvokeRepeating("CreateSun", createSunTime, createSunTime);
    }

    /// <summary>
    /// ��������
    /// </summary>
    private void CreateSun()
    {
        //StartCoroutine(DoCreateSun());
        StartCoroutine(ColorEF(goldWantTime, new Color(1, 0.6f, 0),0.05f, InstantiateSun));

    }
    
    private void InstantiateSun()
    {
        Sun sun = GameManager.Instantiate<GameObject>(GameManager.Instance.GameConf.Sun, transform.position, Quaternion.identity, transform).GetComponent<Sun>();

        //�����������Ծ����
        sun.JumpAnimation();
    }
}
