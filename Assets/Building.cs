using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Building : MonoBehaviour
{
    //settings
    public float maxBuildDistance = 10f;
    
    //references
    public Transform CamChild;

    //build objects
    public Transform floorIndicator;
    public Transform floorBuild;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(CamChild.position, CamChild.forward, out hit, maxBuildDistance))
        {
            //dividing and multiplying by 5 because that's the localscale of the floor and so it snaps into a grid of the size of this object
            //will probably need some tweaking later and then TODO make the localscale a variable
            floorIndicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 5) * 5, Mathf.RoundToInt(hit.point.y) + floorIndicator.localScale.y,
                Mathf.RoundToInt(hit.point.z / 5) *5);
            
            if (Input.GetButtonDown("Fire2"))
            {
                Instantiate(floorBuild, floorIndicator.position, Quaternion.identity);
            }
        }
        
    }
}
