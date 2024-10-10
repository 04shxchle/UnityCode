using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 植物基类
/// </summary>
public  abstract class PlantBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    /// <summary>
    /// 当前植物所在的网格
    /// </summary>
    protected Grid currGrid;
    protected float hp;

    public float Hp { get => hp; }
    public abstract float MaxHp { get;}


    /// <summary>
    /// 查找自身相关组件
    /// </summary>
    protected void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// 创建时的初始化
    /// </summary>
    public void InitForCreate(bool inGrid)
    {
        Find();
        animator.speed = 0;
        if (inGrid)
        {
            spriteRenderer.sortingOrder = -1;
            spriteRenderer.color = new Color(1, 1, 1, 0.6f);
        }
        else
        {
            spriteRenderer.sortingOrder = 1;

        }
        Debug.Log("Planting pea shooter初始化创建");
    }

    /// <summary>
    /// 放置时的初始化
    /// </summary>
    public void InitForPlace(Grid grid)
    {
        hp = MaxHp;
        currGrid = grid;
        currGrid.CurrPlantBase= this;
        transform.position = grid.Position;
        animator.speed = 1;
        spriteRenderer.sortingOrder = 0;
        OnInitForPlace();
        Debug.Log("Planting pea shooter初始化放置");
    }

    /// <summary>
    /// 受伤方法，被僵尸攻击时调用
    /// </summary>
    /// <param name="hurtValue"></param>
    public void Hurt(float hurtValue)
    {
        hp -= hurtValue;
        //发光效果
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        if(Hp<=0)
        {
            //死亡
            Dead();
        }

    }


    /// <summary>
    /// 颜色变化效果
    /// </summary>
    /// <returns></returns>
     protected IEnumerator ColorEF(float wantTime,Color targetColor,float dealyTime,UnityAction fun)
    {
        float currTime = 0;
        float lerp;
        while (currTime < wantTime)
        {
            yield return new WaitForSeconds(dealyTime);
            lerp = currTime / wantTime;
            currTime += dealyTime;
            spriteRenderer.color = Color.Lerp(Color.white, targetColor, lerp);
        }
        spriteRenderer.color = Color.white;
        if(fun!=null) { fun(); }
       

    }

    private void Dead()
    {
        currGrid.CurrPlantBase = null;
        Destroy(gameObject);
    }
    protected virtual void OnInitForPlace()
    {

    }
}
