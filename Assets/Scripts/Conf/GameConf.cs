using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ����
/// </summary>
[CreateAssetMenu(fileName ="GameConf",menuName ="GameConf")]
public class GameConf : ScriptableObject
{
    [Tooltip("����")]
    public GameObject Sun;

    [Tooltip("̫����")]
    public GameObject SunFlower;

    [Tooltip("�㶹����")]
    public GameObject Peashooter;

    [Header("��ʬ")]
    [Tooltip("��ʬ��ͷ")]
    public GameObject Zombie_Head;


    [Header("�ӵ�")]
    [Tooltip("�㶹")]
    public GameObject Bullet1;
    [Tooltip("�㶹����")]
    public Sprite Bullet1Hit;



}
