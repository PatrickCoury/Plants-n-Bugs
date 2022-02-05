using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{

    public int playerNutrients = 0;
    public Text nutText;
    // Update is called once per frame
    void Update()
    {
        nutText.text = "Nutrients: " + playerNutrients;
    }
}
