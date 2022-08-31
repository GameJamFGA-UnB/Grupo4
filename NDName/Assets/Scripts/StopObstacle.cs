using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObstacle : MonoBehaviour
{
    private Collider2D _collider;
    void Start(){
        _collider = GetComponent<Collider2D>();
    }
    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Fan")
        {
            Physics2D.IgnoreCollision(Fan.Instance._collider, _collider);
        }
    }
}
