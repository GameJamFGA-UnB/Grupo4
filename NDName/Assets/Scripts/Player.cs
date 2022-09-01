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
    private bool isWinner = false;
    public Animator _playerAnim { get; private set; }
    public bool isMoving { get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _playerAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Manager.Instance.started)
            return;

        if(isDead || isWinner)
            return;

        if(transform.position.y < 0)
            Die();

        Movement();
    }

    void Movement()
    {
        isMoving =  Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
        isGrounded();

        bool jump = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
        if (jump && _grounded){
            Debug.Log("Jumping");
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce);
            StartCoroutine(ResetJumpRoutine());
            _playerAnim.SetBool("Jumping", true);
        }

        _rigid.velocity = new Vector2((isMoving? 1 : 0) * _speed, _rigid.velocity.y);
        _playerAnim.SetBool("Walking", isMoving);
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
                _playerAnim.SetBool("Jumping", false);
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

    public void Die(){
        if(!_playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Die")){
            Debug.Log("Set trigger die");
            isMoving = false;
            _playerAnim.SetTrigger("Die");
            isDead = true;
            _rigid.velocity = new Vector2(0, _rigid.velocity.y);
        }
    }

    public void Win(){
        Debug.Log("Win");
        isWinner = true;
        isMoving = false;
        _rigid.velocity = new Vector2(0, _rigid.velocity.y);
    }
}
