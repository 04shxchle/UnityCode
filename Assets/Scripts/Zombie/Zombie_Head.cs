using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Head : MonoBehaviour
{
    private Animator animator;
    private bool isOver=false;
    private void Start()
    {
        animator=GetComponent<Animator>();
    }



    void Update()
    {
        if(!isOver&&animator.GetCurrentAnimatorStateInfo(0).normalizedTime>=1)
        {
            //播放完毕
            animator.speed= 0;
            isOver = true;
            //2s之后销毁自身
            Invoke("Destroy",2);

        }
    }
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
