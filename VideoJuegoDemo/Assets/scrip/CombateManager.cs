using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CombateManager : MonoBehaviour
{
    public GameObject[] personajesPrefabs;
    public GameObject[] rivalesPrefabs;

    public Transform posJugador;
    public Transform posRival;

    public Image barraVidaJugador; // UI Image fill (opcional)
    public Image barraVidaRival;   // UI Image fill (opcional)

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
            if (GestorDatos.Instancia != null) GestorDatos.Instancia.SumarVictoria(); // desbloquear siguiente rival
            // aquí puedes mostrar UI de victoria o cargar escena
        }
        else if (muerto == luchadorJugador)
        {
            Debug.Log("Jugador perdió.");
            // mostrar UI de derrota / volver al menú
        }

        // opcional: detener IA/movimientos
        // ejemplo: desactivar scripts
        if (luchadorJugador != null) luchadorJugador.enabled = false;
        if (luchadorRival != null) luchadorRival.enabled = false;
    }

    private void OnDestroy()
    {
        if (luchadorJugador != null) luchadorJugador.OnDeath -= OnLuchadorMuere;
        if (luchadorRival != null) luchadorRival.OnDeath -= OnLuchadorMuere;
    }
}
