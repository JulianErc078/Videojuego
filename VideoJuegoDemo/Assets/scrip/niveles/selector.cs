using UnityEngine;
using UnityEngine.SceneManagement;

public class selector : MonoBehaviour
{
    public void CambiarNivel(string nombreNivel1)
    {
        SceneManager.LoadScene(nombreNivel1);
    }

    public void CambiarNivel1(int nombreNivel1)
    {
        SceneManager.LoadScene(nombreNivel1);
    }
    
}
