using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Singleton<Fan>
{
    [SerializeField]
    private SpriteRenderer sprite;
    private Rigidbody2D _rigid;
    private float _speed = 9.9f;
    private Animator _fanAnim;
    int dir = 1;
    public Collider2D _collider { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _fanAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Instance.transform.localPosition.x < this.transform.localPosition.x){
            dir = -1;
            sprite.flipX = true;
        }
        else{
            dir = 1;
            sprite.flipX = false;
        }

        _rigid.velocity = new Vector2((Player.Instance.isMoving? 1 : 0) *_speed * dir, _rigid.velocity.y);
        _fanAnim.SetBool("Walking", Player.Instance.isMoving);            
    }

    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag == "Player"){
            Debug.Log("Catch");
            Player.Instance.Die();
        }
    }
}
