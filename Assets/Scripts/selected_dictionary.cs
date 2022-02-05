using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selected_dictionary : MonoBehaviour
{
    public Dictionary<int, GameObject> selectedTable = new Dictionary<int, GameObject>();

    public void addSelected(GameObject go)
    {
        int id = go.GetInstanceID();

        if (!(selectedTable.ContainsKey(id)))
        {
            selectedTable.Add(id, go);
            Debug.Log("Added " + id + " to selected dict");
            go.transform.GetComponent<PlayerUnit>().selected = true;
        }
    }

    public void deselect(int id)
    {
        selectedTable.Remove(id);
    }

    public void deselectAll()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedTable)
        {
            if(pair.Value != null)
            {
                try
                {
                    pair.Value.transform.GetComponent<PlayerUnit>().selected = false;
                }
                catch
                {
                    continue;
                }
            }
        }
        selectedTable.Clear();
    }
}
