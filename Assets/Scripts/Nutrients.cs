using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrients : MonoBehaviour
{

    public int nutrientsLeft = 2000;
    public bool occupied = false;
    public List<GameObject> adjVeins;

    private void Start()
    {
        GameObject[] allVeins = GameObject.FindGameObjectsWithTag("Nutrients");
        for(int i = 0; i < allVeins.Length; i++)
        {
            if (Vector3.Distance(this.transform.position, allVeins[i].transform.position) < 500) adjVeins.Add(allVeins[i]);
        }
    }

}
