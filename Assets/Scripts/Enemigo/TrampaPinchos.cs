using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NivelSelector : MonoBehaviour
{
    public GameObject nivelButtonPrefab;
    public Transform buttonContainer;
    public int totalLevels = 10;

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        // Protección: Si no se asignaron los elementos en el Inspector, no rompe el juego
        if (nivelButtonPrefab == null || buttonContainer == null)
        {
            return;
        }

        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject buttonObj = Instantiate(nivelButtonPrefab, buttonContainer);
            
            // Asigna el texto al botón comprobando que exista el componente
            TextMeshProUGUI textoBoton = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
            if (textoBoton != null)
            {
                textoBoton.text = "Nivel " + i;
            }

            int levelIndex = i;
            Button btn = buttonObj.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene("Nivel_" + levelIndex);
                });
            }
        }
    }
}