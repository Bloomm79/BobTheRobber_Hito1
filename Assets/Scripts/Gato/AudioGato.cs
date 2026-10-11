using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioGato : MonoBehaviour
{
    [Header("Clips de Sonido")]
    [Tooltip("Sonido de pasos al caminar")]
    [SerializeField] private AudioClip clipPasos;

    [Tooltip("Sonido al saltar")]
    [SerializeField] private AudioClip clipSalto;

    [Tooltip("Sonido al golpear / hacer daño / arañar")]
    [SerializeField] private AudioClip clipHacerDano;

    [Tooltip("Sonido cuando cae de una zona alta")]
    [SerializeField] private AudioClip clipCaida;

    [Header("Volumen General")]
    [Range(0f, 1f)]
    [SerializeField] private float volumen = 0.8f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = volumen;
    }

    public void ReproducirPasos()
    {
        if (clipPasos == null) return;

        if (!audioSource.isPlaying || audioSource.clip != clipPasos)
        {
            audioSource.clip = clipPasos;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void DetenerPasos()
    {
        if (audioSource.isPlaying && audioSource.clip == clipPasos)
        {
            audioSource.Stop();
        }
    }

    public void ReproducirSalto()
    {
        DetenerPasos();

        if (clipSalto != null)
        {
            audioSource.PlayOneShot(clipSalto, volumen);
        }
    }

    public void ReproducirHacerDano()
    {
        if (clipHacerDano != null)
        {
            audioSource.PlayOneShot(clipHacerDano, volumen);
        }
    }

    public void ReproducirCaida()
    {
        if (clipCaida != null)
        {
            if (Camera.main != null)
            {
                AudioSource.PlayClipAtPoint(clipCaida, Camera.main.transform.position, volumen);
            }
            else
            {
                audioSource.PlayOneShot(clipCaida, volumen);
            }
        }
    }
}