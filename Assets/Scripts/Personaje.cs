
using UnityEngine;

public class Personaje : MonoBehaviour
{
    public GameObject ataque_original;
    public GameObject ataque_posicion;
    public Animator animator;
    public AudioClip salto_clip;
    public AudioClip moneda_clip;
    public AudioClip muerte_clip;
    public AudioClip danio_clip;
    public AudioClip ataque_clip;
    public AudioClip subirNivel_clip;
   

    private AudioSource personaje_AS;

    private bool saltando;
    private bool atacando;
    private bool moviendoDerecha;
    private bool moviendoIzquierda;
    private int vidas;
    private int ultimaActualizacion = 0; // Guarda el valor de monedas cuando se aplicó el último incremento

    public int Vidas {
        get { return vidas; }
        set { vidas = value; }
    }

    private int monedas;

    public int Monedas
    {
        get { return monedas; }
        set { monedas = value; }
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        saltando = false;
        atacando = false;
        vidas = 3;
        monedas = 0;
        personaje_AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || moviendoIzquierda == true)
        {
            transform.Translate(new Vector3(-0.1f, 0.0f));
        }
        else
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || moviendoDerecha == true)
        {
            transform.Translate(new Vector3(0.2f, 0.0f));
        }

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Saltar();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Atacar();
            
        }
    }

    public void Saltar()
    {
        if (saltando == false)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, 450.0f));

            saltando = true;
            animator.SetBool("Saltando", saltando);

            personaje_AS.clip = salto_clip;
            personaje_AS.Play();
        }
    }

    public void Atacar()
    {
        if (atacando == false)
        {
            GameObject.Instantiate(ataque_original, ataque_posicion.transform.position,
                           ataque_posicion.transform.rotation);
            personaje_AS.PlayOneShot(ataque_clip);
            atacando = true;
            animator.SetTrigger("Atacando");
            Invoke("TerminarDeAtacar", 0.2f);
        }
    }

    public void MoverDerecha(bool _activar)
    {
        moviendoDerecha = _activar;
    }

    public void MoverIzquierda(bool _activar)
    {
        moviendoIzquierda = _activar;
    }

    private void TerminarDeAtacar()
    {
        atacando = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            saltando = false;
            animator.SetBool("Saltando", saltando);
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            personaje_AS.clip = danio_clip;
            personaje_AS.Play();
            vidas--;
            animator.SetTrigger("Daño");
            
            if(vidas <= 0)
            {
                personaje_AS.clip = muerte_clip;
                personaje_AS.Play();
                //gameObject.SetActive(false);
                animator.SetBool("Muerto",true);

                int recordUltimo = PlayerPrefs.GetInt("Moneda");
                if(PlayerPrefs.HasKey("Moneda") == false)
                {
                    //No hay record guardado
                    PlayerPrefs.SetInt("Moneda", monedas);
                    Debug.Log("Nuevo Record " + monedas);
                }
                else
                {
                    //Si hay record guardado
                    if(recordUltimo < monedas)
                    {
                        PlayerPrefs.SetInt("Moneda", monedas);
                        Debug.Log("Nuevo Record " + monedas);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Moneda")
        {
            collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.5f);
            monedas++;
            personaje_AS.clip = moneda_clip;
            personaje_AS.Play();
        }

        // Verificamos si el jugador ha recogido 10 monedas más desde la última actualización.
        if (monedas >= ultimaActualizacion + 10)
        {
            personaje_AS.clip = subirNivel_clip;
            personaje_AS.Play();
            Debug.Log("Ejecutar sonido de subir nivel");
            ultimaActualizacion += 10;  // Actualizamos el umbral para el siguiente incremento.
        }
    }
}
