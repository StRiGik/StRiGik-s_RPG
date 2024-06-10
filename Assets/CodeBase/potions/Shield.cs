using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private float _coolDown;
    [SerializeField]private CharacterController _player;

    private Image _shieldImage;

    public bool isCoolDown;

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
                _player.shield.SetActive(false);
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
