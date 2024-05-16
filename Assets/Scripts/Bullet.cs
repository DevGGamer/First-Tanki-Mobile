using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 40f;
    [SerializeField] private float lifetime = 7f;
    [SerializeField] private GameObject ExplosionEffect;
    [SerializeField] private int damage = 10;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) 
    {
       GameObject clone = Instantiate(ExplosionEffect, transform.position, Quaternion.identity);
       Destroy(clone, 4f);

       if (other.TryGetComponent<Health>(out Health health))
            health.GetDamage(damage);

       Destroy(gameObject); 
    }
}
