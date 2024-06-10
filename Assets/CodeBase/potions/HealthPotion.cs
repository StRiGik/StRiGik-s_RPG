using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int _heal;
    [SerializeField] private GameObject _effect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            HelthSystem playerHealth = collision.GetComponent<HelthSystem>();
            playerHealth.AddHP(_heal);
            Instantiate(_effect, collision.transform.position, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
