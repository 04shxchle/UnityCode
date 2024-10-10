using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkySunManager : MonoBehaviour
{
    
    //创建阳光起始的坐标Y
    private float createSunPosY;
    //创建阳光的坐标最左和最右的X,在这个范围随机
    private float createSunMaxPosX = 4.0f;
    private float createSunMinPosX = -7.0f;
    //创建阳光下落的区间最大值Y和最小值Y,在这个范围随机
    private float sunDownMaxPosY = 3.5F;
    private float sunDownMinPosY = -4.0F;
    void Start()
    {
        
        InvokeRepeating("CreateSun", 3,3);
    }

  

    /// <summary>
    /// 从天空中生成阳光
    /// </summary>
    void CreateSun()
    {
        Sun sun=GameObject.Instantiate<GameObject>(GameManager.Instance.GameConf.Sun, Vector3.zero, Quaternion.identity, transform).GetComponent<Sun>();
        float downY=Random.Range(sunDownMinPosY,sunDownMaxPosY);
        float creatX= Random.Range(createSunMinPosX, createSunMaxPosX);
        sun.InitForSky(downY,creatX ,createSunPosY);
    }
}

