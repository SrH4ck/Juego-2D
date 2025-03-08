using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI vidas_txt;
    public TextMeshProUGUI monedas_text;
    public Personaje pj;
    public GameObject gameOver_pn;
    public Generador generadorEnemigos;

  

    public Text record_txt;
    public Text actual_txt;
    private int ultimaActualizacion = 0; // Guarda el valor de monedas cuando se aplicó el último incremento

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vidas_txt.text = pj.Vidas.ToString();
        monedas_text.text = pj.Monedas.ToString();

        if (pj.Vidas <= 0 && gameOver_pn.activeSelf == false)
        {
            gameOver_pn.SetActive(true);
            record_txt.text = PlayerPrefs.GetInt("Moneda").ToString();
            actual_txt.text = pj.Monedas.ToString();
        }
        // Verificamos si el jugador ha recogido 10 monedas más desde la última actualización.
        if (pj.Monedas >= ultimaActualizacion + 10)
        {
            generadorEnemigos.probabilidadDeAparicion += 0.1f;
            ultimaActualizacion += 10;  // Actualizamos el umbral para el siguiente incremento.
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}
