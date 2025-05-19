using System.Collections.Generic;
using UnityEngine;

public class ZonaDanio : MonoBehaviour
{
    public int da�oPorTic = 1;
    public float intervalo = 0.1f;

    private Dictionary<Portador, float> tiempoSiguienteTic = new Dictionary<Portador, float>();

    private void OnTriggerStay(Collider other)
    {
        PortadorJugable portadorJ = other.GetComponent<PortadorJugable>();
        
        if (portadorJ != null)
        {
            // Inicializar si no est� en el diccionario
            if (!tiempoSiguienteTic.ContainsKey(portadorJ))
                tiempoSiguienteTic[portadorJ] = Time.time;

            // Aplicar da�o si pas� el intervalo
            if (Time.time >= tiempoSiguienteTic[portadorJ])
            {
                portadorJ.RecibirDa�o(da�oPorTic);                
                tiempoSiguienteTic[portadorJ] = Time.time + intervalo;
                Debug.Log("El personaje esta recibiendo da�o");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Limpiar del diccionario cuando sale
        Portador portador = other.GetComponent<Portador>();
        if (portador != null && tiempoSiguienteTic.ContainsKey(portador))
        {
            tiempoSiguienteTic.Remove(portador);
        }
    }
}
