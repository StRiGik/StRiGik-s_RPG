using UnityEngine;


public class CharacterController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float _speedCharacter;
    [SerializeField] private float _startDamage;
    [SerializeField] private Joystick _joyStick;

    [Header("Attack Settings")]
    [SerializeField] private Transform _centerAttackRadius;
    [SerializeField] private GameObject _gun;
    [SerializeField] private LayerMask _layerFindEnemys;
    [SerializeField] private float _radiusAttack;

    [SerializeField] private Transform[] _damagePoints;
    [SerializeField] private float _rangeDamagePoints;

    [Header("Shield Settings")]
    [SerializeField] private GameObject _shieldEffect;
    [SerializeField] public GameObject shield;

    [SerializeField] public Shield shieldTimer;


    private Animator _anim;
    private Rigidbody2D _rb;
    private Vector2 _move;
    private bool facingRight = true;
    private bool _isMove;
    private float _currentDamage;

    public bool _isDead;


    private void Start()
    {
        shield.SetActive(false);
        _currentDamage = _startDamage;
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
        if (!_isDead)
        {
            _move = new Vector2(moveX, moveY).normalized * _speedCharacter;
            if (facingRight == true && moveX > 0) { Flip(); }
            else if (facingRight == false && moveX < 0) { Flip(); };
            _rb.velocity = _move;
            AnimateMoving(moveX, moveY);
        } 
        
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
            Collider2D enemysDetected = Physics2D.OverlapCircle(_centerAttackRadius.position, _radiusAttack, _layerFindEnemys);
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
            if (!shield.activeInHierarchy)
            {
                shield.SetActive(true);
                shieldTimer.gameObject.SetActive(true);
                shieldTimer.isCoolDown = true;
            }
            else {shieldTimer.ResetTimer(); }
            Destroy(collision.gameObject);
        }
        
    }

    void DamageEnemys(string value)
    {
        
        switch(value)
        {
            case "UP":
                Collider2D[] enemysUP = Physics2D.OverlapCircleAll(_damagePoints[0].position, _rangeDamagePoints, _layerFindEnemys); 
                foreach(var enemy in enemysUP) { enemy.GetComponent<HelthEnemy>().TakeDamage(_currentDamage); }
                break;
            case "SIDE":
                Collider2D[] enemysSide = Physics2D.OverlapCircleAll(_damagePoints[1].position, _rangeDamagePoints, _layerFindEnemys);
                foreach (var enemy in enemysSide) { enemy.GetComponent<HelthEnemy>().TakeDamage(_currentDamage); }
                break;
            case "DOWN":
                Collider2D[] enemysDown = Physics2D.OverlapCircleAll(_damagePoints[2].position, _rangeDamagePoints, _layerFindEnemys);
                foreach (var enemy in enemysDown) { enemy.GetComponent<HelthEnemy>().TakeDamage(_currentDamage); }
                break;
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(_centerAttackRadius.position, _radiusAttack);
        foreach(var point in _damagePoints)
        {
            Gizmos.DrawWireSphere(point.position, _rangeDamagePoints);
        }
    }


}
