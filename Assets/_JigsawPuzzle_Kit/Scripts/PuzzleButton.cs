using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PuzzleButton : MonoBehaviour
{
    // Event to broadcast when the button is clicked
    public static event UnityAction<string> OnButtonClicked;

    // Start is called before the first frame update
    void Start()
    {
        SetupButton();
    }

    // Separate method for setting up the button and adding the listener
    private void SetupButton()
    {
        // Get the Button component attached to this GameObject
        Button button = GetComponent<Button>();

        // Check if the button component exists
        if (button != null)
        {
            // Add listener to the button click event
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogWarning("PuzzleButton: No Button component found on this GameObject.");
        }
    }

    // Method called when the button is clicked
    private void OnButtonClick()
    {
        // Get the name of the button (this GameObject)
        string buttonName = gameObject.name;

        // Broadcast the event with the button's name
        OnButtonClicked?.Invoke(buttonName);

        // Log to the console
        Debug.Log("Button clicked: " + buttonName);
    }
}