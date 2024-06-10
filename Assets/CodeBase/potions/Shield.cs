using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField]private CharacterController _player;

    public bool isCoolDown;
    private Image _shieldImage;

    private void Start()
    {
        _shieldImage = GetComponent<Image>();
        isCoolDown = true;
    }

    private void Update()
    {
        if (isCoolDown)
        {
            _shieldImage.fillAmount -= 1 / _coolDown * Time.deltaTime;
            if(_shieldImage.fillAmount <= 0)
            {
                _shieldImage.fillAmount = 1;
                isCoolDown = false;
                _player._shield.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
    public void ResetTimer()
    {
        _shieldImage.fillAmount = 1;
    }
    public void ReduceTime(int damage)
    {
        _shieldImage.fillAmount -= damage / 50f;
    }
}
