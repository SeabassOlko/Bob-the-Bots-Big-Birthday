using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float lifetime;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.forward * speed) * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void SetAngle(Quaternion angle)
    {
        transform.rotation = angle;
    }
}
