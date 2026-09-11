using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VidaGato : MonoBehaviour
{
    [Header("Configuración de Vidas")]
    [SerializeField] private int vidas = 3;
    [SerializeField] private Image[] corazonesUI;

    [Header("Efecto Invulnerabilidad")]
    [SerializeField] private float tiempoInvulnerabilidad = 1.2f;
    [SerializeField] private float velocidadParpadeo = 0.1f;

    private bool esInvulnerable = false;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void RecibirDanio(int cantidad)
    {
        if (esInvulnerable) return;

        vidas -= cantidad;
        ActualizarCorazones();

        if (vidas <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            StartCoroutine(RutinaInvulnerabilidad());
        }
    }

    private void ActualizarCorazones()
    {
        if (corazonesUI == null) return;

        for (int i = 0; i < corazonesUI.Length; i++)
        {
            if (corazonesUI[i] != null)
            {
                corazonesUI[i].enabled = (i < vidas);
            }
        }
    }

    private IEnumerator RutinaInvulnerabilidad()
    {
        esInvulnerable = true;

        float tiempoPasado = 0f;
        while (tiempoPasado < tiempoInvulnerabilidad)
        {
            sr.color = new Color(1f, 1f, 1f, 0.2f);
            yield return new WaitForSeconds(velocidadParpadeo);
            sr.color = Color.white;
            yield return new WaitForSeconds(velocidadParpadeo);

            tiempoPasado += (velocidadParpadeo * 2);
        }

        sr.color = Color.white;
        esInvulnerable = false;
    }
}