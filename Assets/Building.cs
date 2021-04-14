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
    public Transform indicator;
    public Transform [] build;

    //privates
    private RaycastHit hit;
    private int chosenBlock;

    // Start is called before the first frame update
    void Start()
    {
        chosenBlock = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            chosenBlock = 0;
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            chosenBlock = 1;
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            chosenBlock = 2;
        }

        if (Physics.Raycast(CamChild.position, CamChild.forward, out hit, maxBuildDistance))
        {
            //dividing and multiplying by 5 because that's the localscale of the floor and so it snaps into a grid of the size of this object
            //will probably need some tweaking later and then
            
            
            //TODO make the localscale a variable
            Debug.DrawLine(CamChild.position, new Vector3(hit.point.x, hit.point.y, hit.point.z), Color.magenta);
            
            indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 5) * 5 -2.5f, Mathf.RoundToInt(hit.point.y)-1 + indicator.localScale.y,
                Mathf.RoundToInt(hit.point.z / 5) * 5 +2.5f);

            //TODO rotate objects correctly
            
            if (Input.GetButtonDown("Fire2"))
            {
                Debug.Log(indicator.position);
                Instantiate(build[chosenBlock], indicator.position, Quaternion.identity);
            }
        }
        
    }
}
