using UnityEngine;

public class CombateManager : MonoBehaviour
{
    public GameObject[] personajesPrefabs; // arrastras los prefabs de personajes aqu� en el Inspector
    public GameObject[] rivalesPrefabs;    // arrastras los prefabs de rivales aqu� en el Inspector

    public Transform posJugador; // posici�n en la escena donde aparecer� el jugador
    public Transform posRival;   // posici�n en la escena donde aparecer� el rival

    void Start()
    {
        int personajeIndex = PlayerPrefs.GetInt("PersonajeSeleccionado", -1);
        int rivalIndex = PlayerPrefs.GetInt("RivalSeleccionado", -1);

        Debug.Log($"Recibido -> Personaje {personajeIndex}, Rival {rivalIndex}");

        if (personajeIndex >= 0 && personajeIndex < personajesPrefabs.Length)
            Instantiate(personajesPrefabs[personajeIndex], posJugador.position, Quaternion.identity);
        else
            Debug.LogError("�ndice de personaje inv�lido");

        if (rivalIndex >= 0 && rivalIndex < rivalesPrefabs.Length)
            Instantiate(rivalesPrefabs[rivalIndex], posRival.position, Quaternion.identity);
        else
            Debug.LogError("�ndice de rival inv�lido");
    }
}