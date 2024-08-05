using UnityEngine;
using TMPro;
using System.Collections; // Ensure this namespace is included for IEnumerator

public class PlayerItemTag : MonoBehaviour
{
    [Header("Tag Settings")]
    public string playerName = "Player"; // Initial player name to be displayed
    public Vector3 offset = new Vector3(0, 2, 0); // Position offset of the text relative to the player
    public float textSize = 1.0f; // Text size

    private Canvas canvas;
    private TextMeshProUGUI textMeshPro;

    void Start()
    {
        // Create a new Canvas object
        GameObject canvasObject = new GameObject("TextCanvas");
        canvasObject.transform.SetParent(transform); // Set as child of the player

        // Add a Canvas component
        canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.WorldSpace;

        // Set the RectTransform for the Canvas
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        canvasRect.sizeDelta = new Vector2(200, 50); // Canvas size
        canvasRect.localPosition = offset;

        // Add a TextMeshPro component for displaying text
        GameObject textObject = new GameObject("TagText");
        textObject.transform.SetParent(canvasObject.transform);

        textMeshPro = textObject.AddComponent<TextMeshProUGUI>();
        textMeshPro.text = playerName; // Initial text is the player's name
        textMeshPro.fontSize = textSize;
        textMeshPro.alignment = TextAlignmentOptions.Center;

        // Set the RectTransform for the text
        RectTransform textRect = textObject.GetComponent<RectTransform>();
        textRect.sizeDelta = new Vector2(200, 50);
        textRect.localPosition = Vector3.zero;
    }

    void LateUpdate()
    {
        // Orient the text to face the camera
        if (Camera.main != null)
        {
            canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        }
    }

    // Method to display item information temporarily
    public void DisplayItemInfo(string itemName, float displayTime = 4f)
    {
        StartCoroutine(ShowTemporaryText(itemName, displayTime));
    }

    // Coroutine to show item information for a specified duration
    private IEnumerator ShowTemporaryText(string itemName, float displayTime)
    {
        textMeshPro.text = itemName;
        yield return new WaitForSeconds(displayTime);
        textMeshPro.text = playerName; // Revert to the player's name after the delay
    }
}
