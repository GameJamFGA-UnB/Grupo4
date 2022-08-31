using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Singleton<Fan>
{
    private Rigidbody2D _rigid;
    private float _speed = 15f;
    public Collider2D _collider { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rigid.velocity = new Vector2((Player.Instance.isMoving? 1 : 0) *_speed, _rigid.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("Catch");
        }
    }
}
