using UnityEngine;

public class CarMover : MonoBehaviour
{
    public float speed = 8f;              // Hastighed
    public float endX = 28f;              // Slutposition på X
    [HideInInspector] public CarManager manager;  // Reference til CarManager

    void Update()
    {
        // Bevæg bilen mod højre i verdensrum
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);

        // Når bilen har passeret endX, send besked til manageren
        if (transform.position.x >= endX)
        {
            if (manager != null)
            {
                manager.NotifyCarReachedEnd(gameObject);
            }
            else
            {
                Debug.LogWarning("CarManager reference mangler på " + gameObject.name);
            }
        }
    }
}
