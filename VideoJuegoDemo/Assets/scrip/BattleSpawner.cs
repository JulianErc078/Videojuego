using UnityEngine;

public class BattleSpawner : MonoBehaviour
{
    [Header("Prefabs de personajes")]
    public GameObject[] personajesPrefabs; // aqu� arrastras TODOS los personajes jugables
    [Header("Prefab del rival de esta escena")]
    public GameObject rivalPrefab; // solo un prefab por escena

    [Header("Posiciones de spawn")]
    public Transform spawnPersonaje;
    public Transform spawnRival;

    void Start()
    {
        // 1. Instanciar personaje seleccionado
        int indicePersonaje = PlayerPrefs.GetInt("PersonajeSeleccionado", 0);
        if (indicePersonaje >= 0 && indicePersonaje < personajesPrefabs.Length)
        {
            Instantiate(personajesPrefabs[indicePersonaje], spawnPersonaje.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("�ndice de personaje inv�lido en PlayerPrefs.");
        }

        // 2. Instanciar el rival (�nico de esta escena)
        if (rivalPrefab != null && spawnRival != null)
        {
            Instantiate(rivalPrefab, spawnRival.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("No se asign� el prefab del rival o la posici�n en la escena.");
        }
    }
}

