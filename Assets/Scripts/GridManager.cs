using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    private List<Vector2>pointList= new List<Vector2>();
    private List<Grid> GridList= new List<Grid>();
   
    private void Awake()
    {
        Instance= this;
        CreateGridBaseGrid();
    }
    void Start()
    {
        //CreatGridsBaseColl();
        //CreateGridBasePointList();
        
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log(GetGidPointByMouse());
        }
    }
    /// <summary>
    /// ������ײ����ʽ��������
    /// </summary>

    private void CreatGridsBaseColl()
    {
        //����һ��Ԥ��������
        GameObject prefabGird = new GameObject();
        prefabGird.AddComponent<BoxCollider2D>().size = new Vector2(1, 1.5f);
        prefabGird.transform.SetParent(transform);
        prefabGird.transform.position=transform.position;
        prefabGird.name = 0 + "-" + 0;
        for(int i=0;i<9;i++)
        {
            for(int j=0;j<5;j++)
            {
                GameObject grid=GameObject.Instantiate<GameObject>(prefabGird,transform.position+new Vector3(1.33f*i,1.63f*j,0),Quaternion.identity,transform);
                grid.name = i + "-" + j;
            }
        }
    }

    /// <summary>
    /// ��������list����ʽ��������
    /// </summary>
    private void CreateGridBasePointList()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                //pointList.Add(transform.position + new Vector3(1.33f*i, 1.63f*j, 0));
             
            }
        }
    }

    /// <summary>
    /// ����Grid�ű�����ʽ��������
    /// </summary>
    private void CreateGridBaseGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 5; j++)
            {
              //  pointList.Add(transform.position + new Vector3(1.33f * i, 1.63f * j, 0));
              GridList.Add(new Grid(new Vector2(i,j),transform.position+new Vector3(1.33f*i,1.63f*j,0),false));
            }
        }
    }
    /// <summary>
    /// ͨ������ȡ���������
    /// </summary>
    /// <returns></returns>
    public Vector2 GetGidPointByMouse()
    {
        return GetGidPointByWorldPos(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /// <summary>
    /// ͨ�����������ȡ���������
    /// </summary>
    public Vector2 GetGidPointByWorldPos(Vector2 worldPos)
    {
        return GetGridByWorldPos(worldPos).Position;
    }
    public Grid GetGridByWorldPos(Vector2 worldPos)
    {
        float dis = 1000000;
        Grid grid=null;
        for (int i = 0; i < GridList.Count; i++)
        {
            if (Vector2.Distance(worldPos, GridList[i].Position) < dis)
            {
                dis = Vector2.Distance(worldPos, GridList[i].Position);
                grid = GridList[i];
            }
        }
        return grid;
    }

    /// <summary>
    /// ͨ��y��Ѱ�����񣬴�������0��ʼ
    /// </summary>
    /// <param name="verticalNum"></param>
    /// <returns></returns>
    public Grid GetGridByVerticalNum(int verticalNum)
    {
        for (int i = 0; i < GridList.Count; i++)
        {
            if (GridList[i].Point == new Vector2(8, verticalNum))
            {
                return GridList[i];
            }
        }
      
        return null;
    }
}
