using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ֲ�����
/// </summary>
public  abstract class PlantBase : MonoBehaviour
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    /// <summary>
    /// ��ǰֲ�����ڵ�����
    /// </summary>
    protected Grid currGrid;
    protected float hp;

    public float Hp { get => hp; }
    public abstract float MaxHp { get;}


    /// <summary>
    /// ��������������
    /// </summary>
    protected void Find()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// ����ʱ�ĳ�ʼ��
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
        Debug.Log("Planting pea shooter��ʼ������");
    }

    /// <summary>
    /// ����ʱ�ĳ�ʼ��
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
        Debug.Log("Planting pea shooter��ʼ������");
    }

    /// <summary>
    /// ���˷���������ʬ����ʱ����
    /// </summary>
    /// <param name="hurtValue"></param>
    public void Hurt(float hurtValue)
    {
        hp -= hurtValue;
        //����Ч��
        StartCoroutine(ColorEF(0.2f, new Color(0.5f, 0.5f, 0.5f), 0.05f, null));
        if(Hp<=0)
        {
            //����
            Dead();
        }

    }


    /// <summary>
    /// ��ɫ�仯Ч��
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
