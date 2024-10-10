using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private Text sunNumText;
    private void Awake()
    {
        Instance= this;
        sunNumText = transform.Find("MainPanel/SunNumText").GetComponent<Text>();
    }
    void Start()
    {
      
    }
    /// <summary>
    /// 更新阳光的数字
    /// </summary>
    public void UpdateSunNum(int num)
    {
        sunNumText.text=num.ToString();
    }
    /// <summary>
    /// 获取阳光数量Text的坐标
    /// </summary>
    public Vector3 GetSunNumTextPos()
    {
        return sunNumText.transform.position;
    }
}
