using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    //攻击力
    private int attackValue;
    //是否击中
    private bool isHit=false;
    public void Init(int attackValue)
    {
        rigidbody=GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody.AddForce(Vector2.right*300);
        this.attackValue = attackValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit) return;
        transform.Rotate(new Vector3(0, 0, -15f));
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (isHit) return;
        if (coll.tag=="Zombie")
        {
            isHit=true;
            
            //让僵尸受伤
            coll.GetComponentInParent<Zombie>().Hurt(attackValue);
            //修改成击中图片
            spriteRenderer.sprite = GameManager.Instance.GameConf.Bullet1Hit;

            //暂停自身运动
            rigidbody.velocity = Vector2.zero;

            //下落
            rigidbody.gravityScale = 1;

            //销毁
            Invoke("Destroy", 0.5f);

        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
