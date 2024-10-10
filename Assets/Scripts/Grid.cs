using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����
/// </summary>
public class Grid 
{
    /// <summary>
    /// ����㣬��0��1����1��1��
    /// </summary>
    public Vector2 Point;
    /// <summary>
    /// ��������
    /// </summary>
    public Vector2 Position;
    /// <summary>
    /// �Ƿ���ֲ�����в�����������ϴ���ֲ��
    /// </summary>
    public bool HavePlant;

    private PlantBase currPlantBase;

    public Grid(Vector2 point, Vector2 position, bool havePlant)
    {
        Point = point;
        Position = position;
        HavePlant = havePlant;
    }
    public PlantBase CurrPlantBase { get => currPlantBase;
        set
        {
            currPlantBase=value;
            if(currPlantBase == null)
            {
                HavePlant = false;
            }
            else
            {
                HavePlant = true;
            }
        }
    }
}
