using UnityEngine;

public class FloaterMover : MonoBehaviour
{
    public float speed = 2f;
    public float endX = 28f;
    [HideInInspector] public FloatManager manager;

    void Update()
    {
        // Bevæg objektet mod højre
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // Når objektet passerer endX, giv besked til manager
        if (transform.position.x >= endX)
        {
            if (manager != null)
            {
                manager.NotifyFloatReachedEnd(gameObject); // <-- korrekt navn
            }
            else
            {
                Debug.LogWarning("FloatManager reference mangler på " + gameObject.name);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(this.transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
