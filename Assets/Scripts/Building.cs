using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform CamChild;

    public Transform [] FloorBuild;  //Indicator
    public Transform [] FloorPrefab; //Block to Instanciate

    private RaycastHit Hit;
    private int chosenBlock;
    
    private Renderer floorRenderer;
    private Renderer stairsRenderer;
    private Renderer wallRenderer;

    private void Awake()
    {
        chosenBlock = 0;
        floorRenderer = FloorBuild[0].GetComponent<Renderer>();
        stairsRenderer = FloorBuild[1].GetChild(0).GetComponent<Renderer>();
        wallRenderer = FloorBuild[2].GetChild(0).GetComponent<Renderer>();
        HideIndicatorBlocks();
    }

    void Update()
    {
        if (Input.GetKeyDown("2")) //floor
        {
            chosenBlock = 0;
            HideIndicatorBlocks();
            floorRenderer.enabled = true;
        }
        else if (Input.GetKeyDown("3")) //ramp
        {
            chosenBlock = 1;
            HideIndicatorBlocks();
            stairsRenderer.enabled = true;
        }
        else if (Input.GetKeyDown("4")) //wall
        {
            chosenBlock = 2;
            HideIndicatorBlocks();
            wallRenderer.enabled = true;
        }
        else if (Input.GetKeyDown("1")) //weapon
        {
            chosenBlock = -1;
            HideIndicatorBlocks();
        }

        if (chosenBlock >= 0)
        {
            if (Physics.Raycast(CamChild.position, CamChild.forward, out Hit, 10f))
            {
                FloorBuild[chosenBlock].position = new Vector3(Mathf.RoundToInt(Hit.point.x / 5) * 5, Mathf.RoundToInt(Hit.point.y),
                    Mathf.RoundToInt(Hit.point.z / 5) * 5);

                FloorBuild[chosenBlock].eulerAngles = new Vector3(0, (Mathf.RoundToInt(transform.eulerAngles.y) != 0 ? Mathf.RoundToInt(transform.eulerAngles.y / 90) * 90 : 0) -90, 0);
            
                if (Input.GetButtonDown("Fire2"))
                {
                    Instantiate(FloorPrefab[chosenBlock], FloorBuild[chosenBlock].position, FloorBuild[chosenBlock].rotation);
                }
            }
        }
    }

    private void HideIndicatorBlocks()
    {
        floorRenderer.enabled = false;
        stairsRenderer.enabled = false;
        wallRenderer.enabled = false;
    }
}
