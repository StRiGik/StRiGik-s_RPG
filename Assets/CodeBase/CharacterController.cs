using Newtonsoft.Json.Bson;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _gun;
    [SerializeField] private Joystick _joyStick;

    private Animator _anim;
    private Rigidbody2D _rb;
    private Vector2 _move;
    private bool facingRight = true;
    private bool _isMove;


    [SerializeField] private Transform _centerPoint;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _radius;
    [SerializeField] private GameObject _shieldEffect;

    [SerializeField] public GameObject _shield;
    [SerializeField] public Shield _shieldTimer;


    private void Start()
    {
        _shield.SetActive(false);
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        MoveHero();
    }

    

    private void MoveHero()
    {
        //float moveX = Input.GetAxisRaw("Horizontal");
        // float moveY = Input.GetAxisRaw("Vertical");
        float moveX = _joyStick.Horizontal;
        float moveY = _joyStick.Vertical;
        
        _move = new Vector2(moveX, moveY).normalized * _speed;
        if(facingRight == true && moveX > 0) { Flip(); }
        else if(facingRight == false && moveX < 0) { Flip(); };
        _rb.velocity = _move;
        AnimateMoving(moveX, moveY);
    } 

    private void AnimateMoving(float moveX, float moveY)
    {
        if (moveX != 0 || moveY != 0)
        {
            _anim.SetBool("walkSide", true);
            _isMove = true;
            _anim.SetBool("swordUp", false);
            _anim.SetBool("swordSide", false);
            _anim.SetBool("swordDown", false);
            _gun.SetActive(false);
        }
        else
        {
            _anim.SetBool("walkSide", false);
            _isMove = false;
            Collider2D enemysDetected = Physics2D.OverlapCircle(_centerPoint.position, _radius, _layerMask);
            if (enemysDetected != null) { AttackEnemy(enemysDetected.transform.position); }
            
            
        }
        
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
    void AttackEnemy(Vector2 enemyPosition)
    {
        if((enemyPosition.x + enemyPosition.y) / Mathf.Sqrt(2) > (transform.position.x + transform.position.y) / Mathf.Sqrt(2) &&
            (enemyPosition.y - enemyPosition.x) / Mathf.Sqrt(2) > (transform.position.y - transform.position.x) / Mathf.Sqrt(2))
        {
            Debug.Log("Враг находится сверху!");
            _gun.SetActive(true);
            if(facingRight == false) { Flip(); }
            _anim.SetBool("swordUp", true);
            _anim.SetBool("swordSide", false);
            _anim.SetBool("swordDown", false);
        }
        else if ((enemyPosition.x + enemyPosition.y) / Mathf.Sqrt(2) > (transform.position.x + transform.position.y) / Mathf.Sqrt(2) &&
                (enemyPosition.y - enemyPosition.x) / Mathf.Sqrt(2) < (transform.position.y - transform.position.x) / Mathf.Sqrt(2))
        {
            Debug.Log("Враг находится справа");
            _gun.SetActive(true);
            if (facingRight == true) { Flip(); }
            _anim.SetBool("swordUp", false);
            _anim.SetBool("swordSide", true);
            _anim.SetBool("swordDown", false);
        }
        else if ((enemyPosition.x + enemyPosition.y) / Mathf.Sqrt(2) < (transform.position.x + transform.position.y) / Mathf.Sqrt(2) &&
                (enemyPosition.y - enemyPosition.x) / Mathf.Sqrt(2) > (transform.position.y - transform.position.x) / Mathf.Sqrt(2))
        {
            Debug.Log("Враг находится слева");
            _gun.SetActive(true);
            if (facingRight == false) { Flip(); }
            _anim.SetBool("swordUp", false);
            _anim.SetBool("swordSide", true);
            _anim.SetBool("swordDown", false);
        }
        else if ((enemyPosition.x + enemyPosition.y) / Mathf.Sqrt(2) < (transform.position.x + transform.position.y) / Mathf.Sqrt(2) &&
                (enemyPosition.y - enemyPosition.x) / Mathf.Sqrt(2) < (transform.position.y - transform.position.x) / Mathf.Sqrt(2))
        {
            Debug.Log("Враг находится снизу");
            _gun.SetActive(true);
            if (facingRight == true) { Flip(); }
            _anim.SetBool("swordUp", false);
            _anim.SetBool("swordSide", false);
            _anim.SetBool("swordDown", true);
        }
    }

    void DethHero()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Shield")
        {
            Instantiate(_shieldEffect, transform.position, Quaternion.identity);
            if (!_shield.activeInHierarchy)
            {
                _shield.SetActive(true);
                _shieldTimer.gameObject.SetActive(true);
                _shieldTimer.isCoolDown = true;
            }
            else {_shieldTimer.ResetTimer(); }
            Destroy(collision.gameObject);
        }
        
    }




}
