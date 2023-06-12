using UnityEngine;

public class DamageOnColPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<PlayerMovement>().isDashing)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
            }
        }


        if (collision.gameObject.GetComponent<PlayerMovement>().isDashing)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
