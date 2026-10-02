using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class GatoCamina : MonoBehaviour
{
    [Header("Movimiento Horizontal")]
    [SerializeField] private float velocidadCaminar = 4.5f;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 6.5f;
    [SerializeField] private Transform detectorSuelo;
    [SerializeField] private float radioDeteccion = 0.25f;
    [SerializeField] private LayerMask capaSuelo;

    [Header("Escaleras (W / S)")]
    [SerializeField] private float velocidadEscalar = 3.5f;

    [Header("Orientación")]
    [SerializeField] private bool spriteOriginalMiraDerecha = true;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    private Collider2D col;
    private AudioGato audioGato;

    private float entradaHorizontal;
    private float entradaVertical;
    private bool estaEnSuelo;
    private bool estaEnEscalera;
    private float gravedadInicial;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        audioGato = GetComponent<AudioGato>();
        gravedadInicial = rb.gravityScale;
    }

    void Update()
    {
        // Controles de entrada
        entradaHorizontal = Input.GetAxisRaw("Horizontal");
        entradaVertical = Input.GetAxisRaw("Vertical");

        // Detección de suelo
        if (detectorSuelo != null)
        {
            estaEnSuelo = Physics2D.OverlapCircle(detectorSuelo.position, radioDeteccion, capaSuelo);
        }
        else if (col != null)
        {
            estaEnSuelo = Physics2D.Raycast(col.bounds.center, Vector2.down, col.bounds.extents.y + 0.15f, capaSuelo);
        }

        // Salto
        if (Input.GetKeyDown(KeyCode.Space) && estaEnSuelo && !estaEnEscalera)
        {
            rb.velocity = new Vector2(rb.velocity.x, fuerzaSalto);

            if (audioGato != null)
            {
                audioGato.ReproducirSalto();
            }
        }

        // Control de animación
        bool seMueveHorizontal = Mathf.Abs(entradaHorizontal) > 0.01f;
        if (anim != null)
        {
            anim.SetBool("estaCaminando", seMueveHorizontal);
        }

        // Control de audio de pasos
        if (audioGato != null)
        {
            bool seMueveVertical = Mathf.Abs(entradaVertical) > 0.01f;
            bool estaMoviendose = (seMueveHorizontal && estaEnSuelo) || (estaEnEscalera && (seMueveHorizontal || seMueveVertical));

            if (estaMoviendose)
            {
                audioGato.ReproducirPasos();
            }
            else
            {
                audioGato.DetenerPasos();
            }
        }

        // Giro del sprite
        if (entradaHorizontal > 0.01f)
        {
            sr.flipX = !spriteOriginalMiraDerecha;
        }
        else if (entradaHorizontal < -0.01f)
        {
            sr.flipX = spriteOriginalMiraDerecha;
        }
    }

    void FixedUpdate()
    {
        if (estaEnEscalera)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(entradaHorizontal * velocidadCaminar, entradaVertical * velocidadEscalar);
        }
        else
        {
            rb.gravityScale = gravedadInicial;
            rb.velocity = new Vector2(entradaHorizontal * velocidadCaminar, rb.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            estaEnEscalera = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Escalera"))
        {
            estaEnEscalera = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (detectorSuelo != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(detectorSuelo.position, radioDeteccion);
        }
    }
}