using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float vel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(vel,0.0f));
    }
}
