using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    //���������
    private int sunNum=0;
    //�����������µ��¼�
    private UnityAction SunNumUpdateAction;
    public int SunNum
    {
        get => sunNum;
        set
        {
            sunNum = value;
            UIManager.Instance.UpdateSunNum(sunNum);
        }
    }
    private void Awake()
    {
        Instance = this;
    }


    /// <summary>
    /// ��������������µ��¼�����
    /// </summary>
    public void AddSunNumUpdateActionListener(UnityAction action)
    {
        SunNumUpdateAction += action;
    }


}

