
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MageMove : MonoBehaviour
{
    public float stepDistance = 20f;        // Hvor langt ét hop er
    public float hopDuration = 1f;       // Hvor lang tid hoppet tager
    public float hopHeight = 1f;         // Hvor højt hoppet går


    private Rigidbody rb;

    private bool isMoving = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    void Update()
    {
        if (isMoving) return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W))
            direction = Vector3.forward;
        else if (Input.GetKeyDown(KeyCode.S))
            direction = Vector3.back;
        else if (Input.GetKeyDown(KeyCode.A))
            direction = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.D))
            direction = Vector3.right;

        if (direction != Vector3.zero)
        {
            Vector3 targetPosition = transform.position + direction * stepDistance;
            StartCoroutine(HopToPosition(targetPosition));
        }
    }

    IEnumerator HopToPosition(Vector3 target)
    {
        isMoving = true;

        Vector3 startPos = transform.position;
        float elapsed = 0f;

        while (elapsed < hopDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / hopDuration;

            // Lerp position + tilføj en simpel parabel (hopbue)
            float height = Mathf.Sin(t * Mathf.PI) * hopHeight;
            transform.position = Vector3.Lerp(startPos, target, t) + Vector3.up * height;

            yield return null;
        }

        transform.position = target;
        isMoving = false;
    }
}