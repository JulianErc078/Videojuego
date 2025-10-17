using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class CombateManager : MonoBehaviour
{
    public GameObject[] personajesPrefabs;
    public GameObject[] rivalesPrefabs;

    public Transform posJugador;
    public Transform posRival;

    public Image barraVidaJugador; // UI Image fill (opcional)
    public Image barraVidaRival;   // UI Image fill (opcional)

    public GameObject panelResultado;
    public TextMeshProUGUI textoResultado;
    public Button btnVolver;

    // instancias actuales
    private GameObject pjInst;
    private GameObject rivalInst;
    private Luchador luchadorJugador;
    private Luchador luchadorRival;

    void Start()
    {
        int personajeIndex = PlayerPrefs.GetInt("PersonajeSeleccionado", -1);
        int rivalIndex = PlayerPrefs.GetInt("RivalSeleccionado", -1);

        Debug.Log($"Cargando combate: personaje={personajeIndex}, rival={rivalIndex}");

        // Instanciar personaje
        if (personajeIndex >= 0 && personajeIndex < personajesPrefabs.Length)
        {
            pjInst = Instantiate(personajesPrefabs[personajeIndex], posJugador.position, Quaternion.identity);
            Debug.Log("Instanciado personaje: " + pjInst.name + " en " + posJugador.position);

            luchadorJugador = pjInst.GetComponent<Luchador>();
            if (luchadorJugador != null)
            {
                luchadorJugador.OnHealthChanged += OnJugadorHealthChanged;
                luchadorJugador.OnDeath += OnLuchadorMuere;
            }
            else
            {
                Debug.LogError("El prefab del jugador no tiene componente Luchador.");
            }
        }
        else
        {
            Debug.LogError("Índice de personaje inválido");
        }

        // Instanciar rival
        if (rivalIndex >= 0 && rivalIndex < rivalesPrefabs.Length)
        {
            rivalInst = Instantiate(rivalesPrefabs[rivalIndex], posRival.position, Quaternion.identity);
            Debug.Log("Instanciado rival: " + rivalInst.name + " en " + posRival.position);

            luchadorRival = rivalInst.GetComponent<Luchador>();
            if (luchadorRival != null)
            {
                luchadorRival.OnHealthChanged += OnRivalHealthChanged;
                luchadorRival.OnDeath += OnLuchadorMuere;
            }
            else
            {
                Debug.LogError("El prefab del rival no tiene componente Luchador.");
            }
        }
        else
        {
            Debug.LogError("Índice de rival inválido");
        }
        if (panelResultado != null)
            panelResultado.SetActive(false);

        if (btnVolver != null)
            btnVolver.onClick.AddListener(() => SceneManager.LoadScene("SelectorNiveles"));
    }



    void OnJugadorHealthChanged(int cur, int max)
    {
        if (barraVidaJugador != null) barraVidaJugador.fillAmount = (float)cur / max;
    }

    void OnRivalHealthChanged(int cur, int max)
    {
        if (barraVidaRival != null) barraVidaRival.fillAmount = (float)cur / max;
    }

    void OnLuchadorMuere(Luchador muerto)
    {
        // Determina vencedor
        if (muerto == luchadorRival)
        {
            Debug.Log("Jugador ganó la pelea!");
            int rivalIndex = PlayerPrefs.GetInt("RivalSeleccionado", 0);
            // aquí puedes mostrar UI de victoria o cargar escena

            if (GestorDatos.Instancia != null)
            {
                GestorDatos.Instancia.SumarVictoria();
                GestorDatos.Instancia.DesbloquearSiguienteRival(rivalIndex);
                Time.timeScale = 0f;
            }
        }
        else if (muerto == luchadorJugador)
        {
            Debug.Log("Jugador perdió.");
            // mostrar UI de derrota / volver al menú
        }

        // opcional: detener IA/movimientos
        // ejemplo: desactivar scripts
        if (luchadorJugador != null)
        {
            luchadorJugador.enabled = false;
            var controlJugador = luchadorJugador.GetComponent<ControlJugador>();
            if (controlJugador != null) controlJugador.enabled = false;
        }

        if (luchadorRival != null)
        {
            luchadorRival.enabled = false;
            var ia = luchadorRival.GetComponent<RivalIA>();
            if (ia) ia.enabled = false;
        }

        if (panelResultado != null && textoResultado != null)
        {
            panelResultado.SetActive(true);

            if (muerto == luchadorRival)
            {
                Debug.Log("🎉 Jugador ganó!");
                int rivalIndex = PlayerPrefs.GetInt("RivalSeleccionado", 0);
                textoResultado.text = "🎉 ¡Ganaste!";
                GestorDatos.Instancia?.SumarVictoria();
            }
            else if (muerto == luchadorJugador)
            {
                Debug.Log("💀 Jugador perdió!");
                StartCoroutine(FinalizarPelea(false));
                textoResultado.text = "💀 Perdiste...";
            }
        }
    }

    private void OnDestroy()
    {
        if (luchadorJugador != null) luchadorJugador.OnDeath -= OnLuchadorMuere;
        if (luchadorRival != null) luchadorRival.OnDeath -= OnLuchadorMuere;
    }

    IEnumerator FinalizarPelea(bool ganoJugador)
    {
        // Esperar un poco para dejar que la animación de morir se vea
        yield return new WaitForSeconds(1.2f);

        Time.timeScale = 0f;

        if (panelResultado != null)
        {
            panelResultado.SetActive(true);
            textoResultado.text = ganoJugador ? "🎉 ¡Ganaste!" : "💀 Perdiste...";
        }
    }

}
