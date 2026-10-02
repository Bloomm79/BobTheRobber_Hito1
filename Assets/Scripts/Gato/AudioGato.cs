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

    [Header("Volumen General")]
    [Range(0f, 1f)]
    [SerializeField] private float volumen = 0.8f;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true; // Permite que camine de forma continua
        audioSource.volume = volumen;
    }

    // Comienza a reproducir el audio de caminar si no estaba sonando ya
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

    // Detiene el sonido de pasos al instante en que el gato frena
    public void DetenerPasos()
    {
        if (audioSource.isPlaying && audioSource.clip == clipPasos)
        {
            audioSource.Stop();
        }
    }

    // Salto usa PlayOneShot para no cortar abruptamente si venía de correr
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
}