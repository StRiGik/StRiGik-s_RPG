using Unity.VisualScripting;
using UnityEngine;

public class HelthSystem : MonoBehaviour
{
    [SerializeField] private int _maxHP;
    [SerializeField]private int _currentHP;
    [SerializeField] HelthBar _helthBar;
    [SerializeField] private Animator _anim;
    [SerializeField] private CharacterController _player;

    private void Start()
    {
        _currentHP = _maxHP;
        _helthBar.SetMaxHealth(_maxHP);
        
    }
    

    public void TakeDamage(int damage)
    {
        if (_player._shield.activeInHierarchy) 
        {
            Debug.Log("я не могу пробить щит!");
            _player._shieldTimer.ReduceTime(damage);
            
        }
        else 
        {
            _currentHP -= damage;
            if (_currentHP <= 0)
            {
                _anim.SetTrigger("deth");
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
    void DethGO()
    {
        Destroy(this.gameObject);
    }
    
}
