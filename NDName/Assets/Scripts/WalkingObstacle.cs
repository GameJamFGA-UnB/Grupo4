using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingObstacle : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform pointA, pointB;
    private SpriteRenderer sprite;
    private Animator anim;
    private Vector3 currentTarget;

    private void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        currentTarget = pointB.position;
    }

    public virtual void Update()
    {
        if(Manager.Instance.state != 2){
            anim.SetBool("Stop", true);
            return;
        }
        anim.SetBool("Stop", false);
        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            return;
        
        Movement();
    }

    public void Movement(){
        if(currentTarget == pointB.position){
            sprite.flipX = true;
        }
        else{
            sprite.flipX = false;
        }

        if(transform.position == pointA.position){
            anim.SetTrigger("Idle");
            currentTarget = pointB.position;
        }
        else if(transform.position == pointB.position){
            anim.SetTrigger("Idle");
            currentTarget = pointA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
    }
}
