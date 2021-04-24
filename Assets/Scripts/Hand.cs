using UnityEngine;

public class Hand : MonoBehaviour
{
    public GameObject bombPrefab;
    public float throwForce = 30;
    public Transform hand;
    public Camera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // throw bomb
        if (Input.GetKeyDown(KeyCode.F))
        { 
            GameObject bomb = Instantiate(bombPrefab, hand.transform.position, Quaternion.identity); 
            bomb.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce, ForceMode.Impulse);  
        }
    }
}
