using UnityEngine;

public class MoverSuelo : MonoBehaviour
{
    
    public float tamSuelo;

    private Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 distancia = mainCamera.transform.position - transform.position;
        if (tamSuelo <= distancia.magnitude)
        {
            transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y, transform.position.z);

        }
    }
}
