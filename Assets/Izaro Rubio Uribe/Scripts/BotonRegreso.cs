using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonRegreso : MonoBehaviour
{
    public void RegresarAEscena0()
    {
        SceneManager.LoadScene(0);  // Número de la Escena 0 en Build Settings
    }
}