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
    /// �������������
    /// </summary>
    public void UpdateSunNum(int num)
    {
        sunNumText.text=num.ToString();
    }
    /// <summary>
    /// ��ȡ��������Text������
    /// </summary>
    public Vector3 GetSunNumTextPos()
    {
        return sunNumText.transform.position;
    }
}
