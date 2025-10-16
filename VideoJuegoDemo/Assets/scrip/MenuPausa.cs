using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject panelPausa;

    private bool juegoPausado = false;

    void Start()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);
    }

    public void TogglePausa()
    {
        if (juegoPausado)
            Reanudar();
        else
            Pausar();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePausa();
    }


    public void Pausar()
    {
        if (panelPausa != null)
            panelPausa.SetActive(true);

        Time.timeScale = 0f; // congela el tiempo del juego
        juegoPausado = true;
    }

    public void Reanudar()
    {
        if (panelPausa != null)
            panelPausa.SetActive(false);

        Time.timeScale = 1f; // reanuda el juego
        juegoPausado = false;
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SelectorNiveles"); // tu escena de selección
    }
}
