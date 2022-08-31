using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    private Rigidbody2D _rigid;
    private BoxCollider2D _boxCollider;
    [SerializeField]
    private float _jumpForce = 50f;
    private bool _resetJump = false;
    private bool _grounded = false;
    [SerializeField]
    private float _speed = 15f;
    private bool isDead = false;
    // private PlayerAnimation _playerAnim;
    public int Health { get; set; }
    public bool isMoving { get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        // _playerAnim = GetComponent<PlayerAnimation>();
        Health = 4;
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
            return;

        Movement();
    }

    void Movement()
    {
        bool move =  Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        isGrounded();

        if(move){
            Debug.Log("Moving");
            isMoving = true;
        } else{
            isMoving = false;
        }

        bool jump = Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (jump && _grounded){
            Debug.Log("Jumping");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            // _playerAnim.Jump(true);
        }

        _rigid.velocity = new Vector2((move? 1 : 0) * _speed, _rigid.velocity.y);
        // _playerAnim.Move(move);
    }

    void isGrounded()
    {
        int layerMask = (1 << 7) | (1 << 8);
        RaycastHit2D hitInfo = Physics2D.BoxCast(
            _boxCollider.bounds.center,
            _boxCollider.bounds.size,
            0f, Vector2.down, .1f, layerMask
        );
        if (hitInfo.collider != null){
            if(!_resetJump){
                // _playerAnim.Jump(false);
                _grounded = true;
                return;
            }
        }
        _grounded = false;
        return;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true;
        yield return new WaitForSeconds(0.1f);
        _resetJump = false;
    }

    public void UpdateSpeed(int value){
        _speed += value;
    }
}
