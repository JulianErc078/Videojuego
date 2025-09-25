using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DatosJugador
{
    public string nombreJugador = "Invitado";
    public int peleasGanadas = 0;
    public List<string> personajesDesbloqueados = new List<string>();
    public List<int> rivalesDesbloqueados = new List<int>();
}

public class GestorDatos : MonoBehaviour
{
    public static GestorDatos Instancia;        // Singleton sencillo
    public DatosJugador datos = new DatosJugador();

    private void Awake()
    {
        // Asegurar una sola instancia que persiste entre escenas
        if (Instancia != null && Instancia != this)
        {
            Destroy(gameObject);
            return;
        }
        Instancia = this;
        DontDestroyOnLoad(gameObject);

        Cargar(); // Cargar al iniciar el juego
    }

    // ---------- API pública cómoda ----------

    public string ObtenerNombre() => datos.nombreJugador;

    public void GuardarNombre(string nuevoNombre)
    {
        datos.nombreJugador = string.IsNullOrWhiteSpace(nuevoNombre) ? "Invitado" : nuevoNombre.Trim();
        Guardar();
         Debug.Log("Guardado nombre: " + datos.nombreJugador);
    }

    public int ObtenerPeleasGanadas() => datos.peleasGanadas;

    public void SumarVictoria()
    {
        datos.peleasGanadas++;

        // Desbloquear siguiente rival (ej: si tienes 5 rivales)
        int siguienteRival = datos.peleasGanadas;
        DesbloquearRival(siguienteRival);

        Guardar();
    }

    public bool PersonajeDesbloqueado(string id) => datos.personajesDesbloqueados.Contains(id);

    public void DesbloquearPersonaje(string id)
    {
        if (!datos.personajesDesbloqueados.Contains(id))
        {
            datos.personajesDesbloqueados.Add(id);
            Guardar();
        }
    }

    // Y agrega este método:
    public bool RivalDesbloqueado(int indexRival)
    {
        if (indexRival == 0) return true; // el primero siempre
        return datos.peleasGanadas >= indexRival;
    }

    // También agrega este método para desbloquear rivales:
    public void DesbloquearRival(int indexRival)
    {
        if (!datos.rivalesDesbloqueados.Contains(indexRival))
        {
            datos.rivalesDesbloqueados.Add(indexRival);
            Guardar();
        }
    }

    public void ReiniciarTodo()
    {
        PlayerPrefs.DeleteKey("NombreJugador");
        PlayerPrefs.DeleteKey("PeleasGanadas");
        PlayerPrefs.DeleteKey("Personajes");
        PlayerPrefs.DeleteKey("Rivales"); // CORREGIDO - comillas dobles

        datos = new DatosJugador();
        // Solo personaje inicial desbloqueado
        datos.personajesDesbloqueados.Add("VENECOS");
        Guardar();
    }



    // ---------- Persistencia (PlayerPrefs) ----------

    private void Guardar()
    {
        PlayerPrefs.SetString("NombreJugador", datos.nombreJugador);
        PlayerPrefs.SetInt("PeleasGanadas", datos.peleasGanadas);
        PlayerPrefs.SetString("Personajes", string.Join(",", datos.personajesDesbloqueados));
        PlayerPrefs.SetString("Rivales", string.Join(",", datos.rivalesDesbloqueados));

        PlayerPrefs.Save();

    }

    private void Cargar()
    {
        datos.nombreJugador = PlayerPrefs.GetString("NombreJugador", "Invitado");
        datos.peleasGanadas = PlayerPrefs.GetInt("PeleasGanadas", 0);

        string csvPersonajes = PlayerPrefs.GetString("Personajes", "");
        datos.personajesDesbloqueados = new List<string>();

        if (string.IsNullOrEmpty(csvPersonajes))
        {
            datos.personajesDesbloqueados.Add("Personaje1");
            datos.personajesDesbloqueados.Add("Personaje2");
            datos.personajesDesbloqueados.Add("Personaje3");
            datos.personajesDesbloqueados.Add("Personaje4");
            datos.personajesDesbloqueados.Add("Personaje5");
            datos.personajesDesbloqueados.Add("Personaje6");
            datos.personajesDesbloqueados.Add("Personaje7");
            datos.personajesDesbloqueados.Add("Personaje8");
        }
        else
        {
            datos.personajesDesbloqueados.AddRange(SplitCSV(csvPersonajes));
        }

        // --- RIVALES (primer rival siempre, otros por victorias) ---
        string csvRivales = PlayerPrefs.GetString("Rivales", "");
        datos.rivalesDesbloqueados = new List<int>();

        if (!string.IsNullOrEmpty(csvRivales))
        {
            foreach (string id in csvRivales.Split(','))
            {
                if (int.TryParse(id, out int rivalId))
                    datos.rivalesDesbloqueados.Add(rivalId);
            }
        }

        // Si es la primera vez y no hay rivales guardados, desbloquear el primero
        if (datos.rivalesDesbloqueados.Count == 0)
            datos.rivalesDesbloqueados.Add(0);

        // Asegurar nombre por defecto
        if (string.IsNullOrWhiteSpace(datos.nombreJugador))
            datos.nombreJugador = "Invitado";

        Debug.Log("Cargado nombre: " + datos.nombreJugador);
    }

    private List<string> SplitCSV(string csv)
    {
        var lista = new List<string>();
        var partes = csv.Split(',');
        foreach (var p in partes)
        {
            var t = p.Trim();
            if (!string.IsNullOrEmpty(t) && !lista.Contains(t))
                lista.Add(t);
        }
        return lista;
    }
}
