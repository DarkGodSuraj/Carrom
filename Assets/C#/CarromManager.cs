using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarromManager : MonoBehaviour
{
    public List<Transform> pockets = new List<Transform>(); //A list to store pocket Transforms

    public void Awake()
    {
        //Loop through all children and find a specific tag name, adding it to the list
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Pocket"))
            {
                pockets.Add(child);
            }
        }
    }
}

