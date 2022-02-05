using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : MonoBehaviour
{
    public bool selected = false;
    
    public float unitSpeed = 20f;
    public Vector3 moveTo;
    

    public NavMeshAgent navMeshAgent;
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = unitSpeed;
        var outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = Color.green;
        outline.OutlineWidth = 3f;

    }

    // Update is called once per frame
    void Update()
    {
        if (selected) GetComponent<Outline>().enabled = true;
        else GetComponent<Outline>().enabled = false;
        if (Input.GetMouseButton(1) && selected) RightMouseDown();
    }
    private void RightMouseDown()
    {
        Plane plane = new Plane(Vector3.up, 0);
        float distance;
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                navMeshAgent.destination = ray.GetPoint(distance);//integration with navmeh
            }
        }
    }
}
