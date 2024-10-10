using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //�����Ŀ��
    private float downTargetPosY;
    //�������
    public bool isFromSky;
    // Update is called once per frame
    void Update()
    {
        if(!isFromSky)
        {
            return;
        }
        if(transform.position.y <=  downTargetPosY)
        {
            Invoke("DestroySun", 10);
            return;
        }
        transform.Translate(Vector3.down* Time.deltaTime);
    }
    /// <summary>
    /// ����������ʱ��������Ϸ�������е���������
    /// </summary>
    private void OnMouseDown()
    {
        PlayerManager.Instance.SunNum += 25;
        UIManager.Instance.GetSunNumTextPos();
        Vector3 sunNum=Camera.main.ScreenToWorldPoint(UIManager.Instance.GetSunNumTextPos());
        sunNum = new Vector3(sunNum.x, sunNum.y, 0);
        FlyAnimation(sunNum);
        
    }
    /// <summary>
    /// ��Ծ����
    /// </summary>
    public void JumpAnimation()
    {
        //�϶�����̫����
        isFromSky = false;
        StartCoroutine(DoJump());
    }
    private IEnumerator DoJump()
    { 
        bool isLeft = Random.Range(0, 2) == 0;
        Vector3 startPos = transform.position;
        float x;
        if(isLeft)
        {
            x = -0.1f;
        }
        else
        {
            x = 0.1f;
        }
        float speed = 0;
        while (transform.position.y <= startPos.y + 1)
        {
            yield return new WaitForSeconds(0.005f);
            speed += 0.001f;
            transform.Translate(new Vector3(x, 0.05f+speed, 0));
        }
        while (transform.position.y >= startPos.y + 1)
        {
            yield return new WaitForSeconds(0.005f);
            speed += 0.001f;
            transform.Translate(new Vector3(x, -0.05f-speed, 0));
        }
        
    }


    /// <summary>
    /// ���ж���
    /// </summary>
    private void FlyAnimation(Vector3 pos)
    {
        StartCoroutine(DoFly(pos));
    }


    private IEnumerator DoFly(Vector3 pos)
    {
        Vector3 direction= (pos - transform.position).normalized;
        while (Vector3.Distance(pos, transform.position) > 0.75f)
        {
            yield return new WaitForSeconds(0.01f);
            transform.Translate(direction);
        }
        DestroySun();
    }
    /// <summary>
    /// �ݻ�����
    /// </summary>
    private void DestroySun()
    {
        Destroy(gameObject);
    }
    ///<summary>
    ///�����������г�ʼ���ķ���
    /// </summary>
    public void InitForSky(float downTargetPosY,float creatPosX,float CreatPosY)
    {
        this.downTargetPosY = downTargetPosY;
        transform.position = new Vector2(creatPosX, CreatPosY);
        isFromSky = true;
    }
}
