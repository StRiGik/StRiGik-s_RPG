using UnityEngine;

public class HelthEnemy : MonoBehaviour
{
    [SerializeField] private float _startHP;
    [SerializeField] private float _currentHP;

    [SerializeField] private GameObject _damageEffect;


    private void Start()
    {
        _currentHP = _startHP;
    }

    public void TakeDamage(float damage)
    {
        _currentHP -= damage;
        Instantiate(_damageEffect, transform.position, Quaternion.identity);
        if (_currentHP <= 0) { Destroy(this.gameObject); }
    }

}
