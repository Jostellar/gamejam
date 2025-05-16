using UnityEngine;

public class youwin : MonoBehaviour
{
    public GameObject Youwin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       Youwin.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        Youwin.SetActive(true);
    }
}
