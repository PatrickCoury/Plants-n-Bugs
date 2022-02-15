using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    public bool harvesting = false;
    public bool carrying = false;
    public GameObject eventHandler;
    private RaycastHit hit;
    private PlayerUnit pu;
    private GameObject nVein;
    private GameObject closestHill;
    private void Start()
    {
       pu = GetComponent<PlayerUnit>(); 
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)&&pu.selected)
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 50000.0f))
            {
                if (hit.transform.gameObject.tag == "Nutrients")//if nutrients is right-clicked
                {
                    harvesting = true;
                    nVein = hit.transform.gameObject;
                    float dist = 1000000f;
                    GameObject[] antHills = GameObject.FindGameObjectsWithTag("Anthill");
                    closestHill = antHills[0];
                    foreach (GameObject hill in antHills)
                    {
                        if (Vector3.Distance(transform.position, hill.transform.position) < dist) closestHill = hill;//find the closest anthill
                    }
                }
                else
                {
                    clear();
                }
            }
            
        }
       
        if (harvesting && Vector3.Distance(transform.position,nVein.transform.position) < 1.25)
        {

            if (!carrying)
            {
                StartCoroutine(harvestLoop());
            }
            else
            {
                pu.navMeshAgent.destination = closestHill.transform.position;
            }
        }
        if (harvesting && carrying && Vector3.Distance(transform.position, closestHill.transform.position) < 5)
        {
            eventHandler.GetComponent<PlayerResources>().playerNutrients++;
            carrying = false;
            pu.navMeshAgent.destination = nVein.transform.position;
        }

    }

    void clear()
    {
        harvesting = false;
        nVein = null;
    }
    IEnumerator harvestLoop()
    {
        Nutrients nutrients = nVein.GetComponent<Nutrients>();
        while (nutrients.occupied)
        {
            foreach(GameObject vein in nutrients.adjVeins)
            {
                if (!vein.GetComponent<Nutrients>().occupied)
                {
                    nVein = vein;
                    pu.navMeshAgent.destination = vein.transform.position;
                    yield break;
                }
            }
            yield return new WaitForSeconds(.5f);
        }
        nutrients.occupied = true;
            harvesting = false;
            pu.navMeshAgent.destination = transform.position;
            yield return new WaitForSeconds(5);

            if (nutrients.nutrientsLeft > 0&& Vector3.Distance(pu.navMeshAgent.destination,transform.position)<=1)
            {
             
                nutrients.nutrientsLeft--;//subtract a nutrient
                carrying = true;
                pu.navMeshAgent.destination = closestHill.transform.position;
                
                yield return new WaitForSeconds(.2f);
                harvesting = true;

        }
    }
}
