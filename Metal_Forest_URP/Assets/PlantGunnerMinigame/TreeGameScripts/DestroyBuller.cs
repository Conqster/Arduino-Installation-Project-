using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBuller : MonoBehaviour
{
    [SerializeField] private float timeBeforeDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("DestroyBullet", timeBeforeDestroyed,0);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().DealDamage(1);
            Debug.Log("BulletHit");
            Destroy(gameObject);
        }
    }
    
}
