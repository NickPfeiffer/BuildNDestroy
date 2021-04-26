using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay;
    public float explosionRadius;

    public float damage;

    public GameObject particlePrefab;
    private GameObject particleInstance;
    
    void Start()
    {
        //execute method after given delay
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                if (c.GetComponent<Collider>().CompareTag("Enemy"))
                {
                    c.transform.GetComponent<EnemyAi>().AiTakeDamage(damage);
                }

                Target target = c.transform.GetComponent<Target>();
                
                //only make object take damage/destroy object if it has the target script
                if (target != null)
                {
                    target.TakeDamage(damage);
                }
            }
        }

        particleInstance = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().Play();

        //stop rendering and colliding
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        
        Invoke(nameof(Kill), 5);
    }
    
    void Kill()
    {
        Destroy(particleInstance);
        Destroy(gameObject);
    }
}
