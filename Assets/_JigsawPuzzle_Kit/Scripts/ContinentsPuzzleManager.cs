using UnityEngine;
using System.Collections.Generic;

public class ContinentsPuzzleManager : MonoBehaviour
{
    // List to hold the GameObjects
    public List<GameObject> continentObjects;

    // Start is called once before the first frame update
    void Start()
    {
        // Fetch the index of the button clicked from the previous scene
        int index = ChangeScene.GetButtonClickedValue();

        // Check if the index is valid and within the bounds of the list
        if (index >= 0 && index < continentObjects.Count)
        {
            // Disable all objects first (optional)
            foreach (GameObject obj in continentObjects)
            {
                obj.SetActive(false);
            }

            // Enable the object at the given index
            continentObjects[index].SetActive(true);
        }
        else
        {
            Debug.LogError("Index out of bounds: " + index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Any update logic can go here
    }
}