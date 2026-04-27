using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject explosion;
    private float projectileLife = 1f;
    private bool hasExploded = false;

    public AudioSource audioSource;
    public AudioClip explosionSound;

    //explodes the projectile after lifetime
    void Start()
    {
        Invoke("explode", projectileLife);
    }

    //explodes immediately if projectile hits
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Ground"))
        {
            explode();
        }
    }

    //explosion logic, only allows one explosion, plays boom audio
    void explode()
    {
        if (hasExploded)
        {
            return;
        }

        if (audioSource != null && explosionSound != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(explosionSound);
        }

        hasExploded = true;
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
