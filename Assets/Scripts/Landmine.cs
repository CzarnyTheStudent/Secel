using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Landmine : MonoBehaviour
{
    public float explosionRadius = 5f;
    public float explosionDesapear = 0.6f;
    //public float explosionForce = 700f;
    public float countdownTime = 3f;
    public GameObject explosionEffect;
    
    private Light2D _lightMine;
    private bool _exploded = false;

    private void Start()
    {
        _lightMine = GetComponent<Light2D>();
        _lightMine.intensity = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_exploded)
        {
            StartCountdown();
        }
    }

    private void StartCountdown()
    {
        _exploded = true;
        LightBlinking();
        // Start the countdown
        StartCoroutine(ExplodeAfterCountdown());
    }

    private System.Collections.IEnumerator ExplodeAfterCountdown()
    {
        yield return new WaitForSeconds(countdownTime);

        Explode();
    }
    private void LightBlinking()
    {
        _lightMine.intensity = Mathf.PingPong(Time.time, 8);
    }
    

    private void Explode()
    {
        // Create explosion effect
        GameObject clone = Instantiate(explosionEffect, transform.position, transform.rotation);
        //colliders.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        Destroy(clone, 0.5f);
        // Get all objects within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D nearbyObject in colliders)
        {
            if (nearbyObject.CompareTag("Player"))
            {
                nearbyObject.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            }
            Rigidbody2D rb = nearbyObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                
                // Apply explosion force
                Vector2 direction = rb.transform.position - transform.position;
                //rb.AddForce(direction.normalized * explosionForce, ForceMode2D.Impulse);
            }
        }
        StopAllCoroutines();
        // Destroy the landmine
        Destroy(gameObject);
    }
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
#endif
}