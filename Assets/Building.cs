using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform CamChild;

    public Transform FloorBuild;  //Indicator
    public Transform FloorPrefab; //Block to Instanciate

    private RaycastHit Hit;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, 10f))
        {
            FloorBuild.position = new Vector3(Mathf.RoundToInt(Hit.point.x / 5) * 5, Mathf.RoundToInt(Hit.point.y),
                Mathf.RoundToInt(Hit.point.z / 5) * 5);

            FloorBuild.eulerAngles = new Vector3(0, (Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(transform.eulerAngles.y / 90) * 90 : 0) -90, 0);
            
            if (Input.GetButtonDown("Fire2"))
            {
                Instantiate(FloorPrefab, FloorBuild.position, FloorBuild.rotation);
            }
        }
    }
}
