using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ÓÎÏ·ÅäÖÃ
/// </summary>
[CreateAssetMenu(fileName ="GameConf",menuName ="GameConf")]
public class GameConf : ScriptableObject
{
    [Tooltip("Ñô¹â")]
    public GameObject Sun;

    [Tooltip("Ì«Ñô»¨")]
    public GameObject SunFlower;

    [Tooltip("Íã¶¹ÉäÊÖ")]
    public GameObject Peashooter;

    [Header("½©Ê¬")]
    [Tooltip("½©Ê¬µÄÍ·")]
    public GameObject Zombie_Head;


    [Header("×Óµ¯")]
    [Tooltip("Íã¶¹")]
    public GameObject Bullet1;
    [Tooltip("Íã¶¹»÷ÖĞ")]
    public Sprite Bullet1Hit;



}
