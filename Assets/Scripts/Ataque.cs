using UnityEngine;

public class Ataque : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemigo")
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            gameObject.SetActive(false);
            Destroy(gameObject, 0.1f);
        }
    }
}
