using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
    
    //����������ʼ������Y
    private float createSunPosY;
    //���������������������ҵ�X,�������Χ���
    private float createSunMaxPosX = 4.0f;
    private float createSunMinPosX = -7.0f;
    //��������������������ֵY����СֵY,�������Χ���
    private float sunDownMaxPosY = 3.5F;
    private float sunDownMinPosY = -4.0F;
    void Start()
    {
        
        InvokeRepeating("CreateSun", 3,3);
    }

  

    /// <summary>
    /// ���������������
    /// </summary>
    void CreateSun()
    {
        Sun sun=GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Sun, Vector3.zero, Quaternion.identity, transform).GetComponent<Sun>();
        float downY=Random.Range(sunDownMinPosY,sunDownMaxPosY);
        float creatX= Random.Range(createSunMinPosX, createSunMaxPosX);
        sun.InitForSky(downY,creatX ,createSunPosY);
    }
}

