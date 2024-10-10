using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    //下落的目标
    private float downTargetPosY;
    //来自天空
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
    /// 鼠标点击阳光的时候，增加游戏管理器中的阳光数量
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
    /// 跳跃动画
    /// </summary>
    public void JumpAnimation()
    {
        //肯定来自太阳花
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
    /// 飞行动画
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
    /// 摧毁自身
    /// </summary>
    private void DestroySun()
    {
        Destroy(gameObject);
    }
    ///<summary>
    ///当阳光从天空中初始化的方法
    /// </summary>
    public void InitForSky(float downTargetPosY,float creatPosX,float CreatPosY)
    {
        this.downTargetPosY = downTargetPosY;
        transform.position = new Vector2(creatPosX, CreatPosY);
        isFromSky = true;
    }
}
