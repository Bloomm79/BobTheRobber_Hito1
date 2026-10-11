using UnityEngine;

public class DanioPorCaida : MonoBehaviour
{
    [Header("Ajustes de Caida")]
    [SerializeField] private float velocidadLimite = -4f;
    [SerializeField] private int danio = 1;

    private Rigidbody2D rb;
    private VidaGato vidaGato;
    private AudioGato audioGato;
    private float velocidadVerticalPrevia;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        vidaGato = GetComponent<VidaGato>();
        audioGato = GetComponent<AudioGato>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.y < -0.1f)
{
    velocidadVerticalPrevia = rb.velocity.y;
}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ComprobarContacto(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ComprobarContacto(collision.gameObject);
    }

    private void ComprobarContacto(GameObject obj)
    {
        if (obj.layer == LayerMask.NameToLayer("suelo") || obj.CompareTag("Suelo"))
        {
            if (velocidadVerticalPrevia <= velocidadLimite)
            {
                if (audioGato != null)
                {
                    audioGato.ReproducirCaida();
                }

                if (vidaGato != null)
                {
                    vidaGato.RecibirDanio(danio);
                }
            }
        }
    }
}