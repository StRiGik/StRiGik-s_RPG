using UnityEngine;

public class HelthSystem : MonoBehaviour
{
    [Header("Helth Settings")]
    [SerializeField] private int _maxHP;
    [SerializeField] HelthBar _helthBar;

    [Header("Animation Settings")]
    [SerializeField] private Animator _anim;
    [SerializeField] private CharacterController _player;

    private int _currentHP;


    private void Start()
    {
        _currentHP = _maxHP;
        _helthBar.SetMaxHealth(_maxHP);
        
    }
    

    public void TakeDamage(int damage)
    {
        if (_player.shield.activeInHierarchy) 
        {
            Debug.Log("я не могу пробить щит!");
            _player.shieldTimer.ReduceTime(damage);
            
        }
        else 
        {
            _currentHP -= damage;
            if (_currentHP <= 0)
            {
                _anim.SetTrigger("deth");
                _player._isDead = true;
            }
            else if (damage != 0)
            {
                _anim.SetTrigger("takeDamage");
            }

        }
        
        
        
        
        _helthBar.Sethealth(_currentHP);
    }


    public void AddHP(int newHelth)
    {
        _currentHP += newHelth;
        if(_currentHP > _maxHP) { _currentHP = _maxHP; }
        _helthBar.Sethealth(_currentHP);
    }

    
}
