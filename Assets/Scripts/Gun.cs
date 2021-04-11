using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public Camera cam;
    public GameObject gun;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    //line is drawn if ray hits an object
    // currently ray is shot where camera is looking, but player needs to turn when camera turns too
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(gun.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.DrawLine(gun.transform.position, hit.point, Color.green);
            Debug.Log(hit.transform.name);
        }
    }
}
