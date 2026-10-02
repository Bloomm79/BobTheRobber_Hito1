using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jugar()
    {
        // "SampleScene" es el nombre exacto de tu escena de juego
        SceneManager.LoadScene("SampleScene");
    }
}