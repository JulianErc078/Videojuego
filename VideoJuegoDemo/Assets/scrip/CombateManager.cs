using UnityEngine;

public class CombateManager : MonoBehaviour
{
    public GameObject[] personajesPrefabs; // arrastras los prefabs de personajes aquí en el Inspector
    public GameObject[] rivalesPrefabs;    // arrastras los prefabs de rivales aquí en el Inspector

    public Transform posJugador; // posición en la escena donde aparecerá el jugador
    public Transform posRival;   // posición en la escena donde aparecerá el rival

    void Start()
    {
        int personajeIndex = PlayerPrefs.GetInt("PersonajeSeleccionado", -1);
        int rivalIndex = PlayerPrefs.GetInt("RivalSeleccionado", -1);

        Debug.Log($"Recibido -> Personaje {personajeIndex}, Rival {rivalIndex}");

        if (personajeIndex >= 0 && personajeIndex < personajesPrefabs.Length)
            Instantiate(personajesPrefabs[personajeIndex], posJugador.position, Quaternion.identity);
        else
            Debug.LogError("Índice de personaje inválido");

        if (rivalIndex >= 0 && rivalIndex < rivalesPrefabs.Length)
            Instantiate(rivalesPrefabs[rivalIndex], posRival.position, Quaternion.identity);
        else
            Debug.LogError("Índice de rival inválido");
    }
}