using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private RectTransform crosshair;
    //[Range(56f,120f)]

    public float speed;
    public float currentSize;
    public float minSize = 60f; 
    public float maxSize = 120f; 
    public Color objectColor; // The color you want to change to
    public Color originalColor, enemyColor;
    public GameObject[] objectsToChangeColor; // Array to hold references to objects

    // Start is called before the first frame update
    void Start()
    {
        crosshair = GetComponent<RectTransform>();

    }

    // Update is called once per frame
    void Update()
    {
        // Cast a ray from the camera's center to detect the object
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0)), out hit))
        {
            // Calculate the distance to the hit object
            float distanceToHitObject = hit.distance;
            if(distanceToHitObject< 25f && hit.collider.CompareTag("Environment")){
                currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime*speed);
                ChangeColors(objectColor);
            }
            else{
                currentSize = Mathf.Lerp(currentSize, 90f, Time.deltaTime*speed);
                ChangeColors(originalColor);
            }

            if(hit.collider.CompareTag("Enemy")){
                currentSize = Mathf.Lerp(currentSize, minSize, Time.deltaTime*speed);
                ChangeColors(enemyColor);
            }

            
            
        }
        else{
            currentSize = Mathf.Lerp(currentSize, maxSize, Time.deltaTime*speed);
            ChangeColors(originalColor);
        }
        crosshair.sizeDelta = new Vector2(currentSize,currentSize);

    }


    public void ChangeColors(Color newColor)
    {
        foreach (GameObject obj in objectsToChangeColor)
        {
            // Check if the object has a Renderer (for 3D objects)
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material.color = newColor;
            }

            // Check if the object has an Image (for UI objects)
            UnityEngine.UI.Image image = obj.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.color = newColor;
            }
        }
    }

    
}
