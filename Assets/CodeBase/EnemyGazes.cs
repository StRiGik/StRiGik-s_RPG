using UnityEngine;

public class EnemyGazes : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private float _timer;
    [SerializeField] private int _startDamage;
    
    private int _damage;
    private HelthSystem _playerhelth;
    private bool _activatedSmokeDamage;


    private void Start()
    {
        _damage = _startDamage;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _playerhelth = collision.GetComponent<HelthSystem>();
            _activatedSmokeDamage = true;
            _damage = _startDamage;
            Debug.Log("он зашел!");
            Invoke("PoisonAttack", _timer);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _activatedSmokeDamage = false;
            _damage = 0;
        }
    }


    private void PoisonAttack()
    {
        Debug.Log("запущен ГАЗ!");
        _playerhelth.TakeDamage(_damage);
        if (_activatedSmokeDamage) { Invoke("PoisonAttack", _timer); }
    }

}