using UnityEngine;

public class GunFPS : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Camera fpsCam;
    public GameObject gun;
    public ParticleSystem muzzleFlash;

    private float nextTimetoFire = 0f;
    
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
        
        RaycastHit hitInfo;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            Debug.DrawLine(gun.transform.position, hitInfo.point, Color.green);
            Debug.Log(hitInfo.transform.name);
            
            Target target = hitInfo.transform.GetComponent<Target>();
            
            //only make object take damage/destory object if it has the target script
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
