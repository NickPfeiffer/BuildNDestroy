using UnityEngine;

public class GunFPS : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public GameObject gun;
    public ParticleSystem muzzleFlash;
    public AudioSource firingSound;

    private float nextTimetoFire = 0.2f;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        gameObject.GetComponent<AudioSource>().PlayOneShot(firingSound.clip);

        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            Debug.DrawLine(gun.transform.position, hitInfo.point, Color.green);
            Debug.Log(hitInfo.transform.name);
            
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.transform.GetComponent<EnemyAi>().AiTakeDamage(damage);
            }

            Target target = hitInfo.transform.GetComponent<Target>();

            //only make object take damage/destroy object if it has the target script
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
