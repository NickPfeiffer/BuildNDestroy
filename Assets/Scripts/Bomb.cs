using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float delay;
    public float explosionRadius;
    public float explosionForce;
    public float upModifier;

    public LayerMask interactionMask;

    public GameObject particlePrefab;
    private GameObject particleInstance;
    
    void Start()
    {
        //execute method after given delay
        Invoke(nameof(Explode), delay);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, interactionMask);
        if (colliders.Length > 0)
        {
            foreach (Collider c in colliders)
            {
                Debug.Log(c);
                c.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, upModifier, ForceMode.Impulse);
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
