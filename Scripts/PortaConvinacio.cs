using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortaConvinacio : MonoBehaviour
{
    public GameObject porta;
    public float distanciaAmunt = 3.5f;
    public float duracio = 2f;
    private Vector3 posicioOriginal;

    public GameObject panelConvinacio;
    public GameObject tagClickE;
    public TMP_Text convinacio;

    private string combinacionUsuario = "";

    private void Update()
    {
        // Cerrar el panel con Escape
        if (panelConvinacio.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            panelConvinacio.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        // Capturar eventos del teclado cuando el panel de combinación está activo
        if (panelConvinacio.activeSelf)
        {
            CapturarTeclas();
        }

        // Verificar si la combinación ingresada es "1234"
        if (combinacionUsuario == "1234")
        {
            panelConvinacio.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            StartCoroutine(MoureAmunt(porta, posicioOriginal + new Vector3(0, distanciaAmunt, 0)));
            combinacionUsuario = ""; // Reiniciar la combinación
        }
    }

    private void Start()
    {
        posicioOriginal = porta.transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tagClickE.SetActive(true);
        }

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            panelConvinacio.SetActive(true);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tagClickE.SetActive(false);
        StartCoroutine(MoureAmunt(porta, posicioOriginal));
    }

    private IEnumerator MoureAmunt(GameObject objeto, Vector3 posicionFinal)
    {
        Vector3 posicionInicial = objeto.transform.position;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracio)
        {
            objeto.transform.position = Vector3.Lerp(posicionInicial, posicionFinal, tiempoTranscurrido / duracio);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        objeto.transform.position = posicionFinal;
    }

    // Función para capturar teclas numéricas
    private void CapturarTeclas()
    {
        foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
        {
            // Solo permitir números si la longitud de la combinación es menor a 4
            if (Input.GetKeyDown(keyCode) && keyCode >= KeyCode.Alpha0 && keyCode <= KeyCode.Alpha9 && combinacionUsuario.Length < 4)
            {
                string numero = keyCode.ToString().Replace("Alpha", "");
                combinacionUsuario += numero;
                convinacio.text = combinacionUsuario;
            }

            // Borrar el último número con la tecla de retroceso
            if (Input.GetKeyDown(KeyCode.Backspace) && combinacionUsuario.Length > 0)
            {
                combinacionUsuario = combinacionUsuario.Substring(0, combinacionUsuario.Length - 1);
                convinacio.text = combinacionUsuario;
            }
        }
    }
}
