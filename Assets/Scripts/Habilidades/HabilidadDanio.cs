using UnityEngine;

[CreateAssetMenu(fileName = "HabilidadDanio", menuName = "Scriptable Objects/HabilidadDanio")]
public class HabilidadDanio : Habilidad
{
    [Header("Par�metros de Da�o")]
    public int da�o;
    public float radio;
    public float duracion;
    //public GameObject areaVisualPrefab;
    [Header("VFX y Audio")]
    public GameObject efectoVisual;

    public override void Ejecutar(Portador objetivo)
    {
        if (!PuedeUsarse(Time.time)) return;

        Vector3 posicionImpacto = objetivo.transform.position + objetivo.transform.forward * 3f; // 3m frente a ti
        posicionImpacto.y = 0.1f;

        //// Instanciar efecto visual (c�rculo o fuego)
        //if (areaVisualPrefab != null)
        //{
        //    GameObject visual = GameObject.Instantiate(areaVisualPrefab, posicionImpacto, Quaternion.identity);
        //    visual.transform.localScale = Vector3.one * radio * 2f; // ajustar al radio real
        //    GameObject.Destroy(visual, duracion);
        //}
        // VFX m�gico
        if (efectoVisual != null)
        {
            GameObject fx = Instantiate(efectoVisual, posicionImpacto, Quaternion.identity);
            Destroy(fx, duracion); // Auto-destruir
        }

        // Detectar y da�ar enemigos en el �rea
        Collider[] colisiones = Physics.OverlapSphere(posicionImpacto, radio);
        foreach (var col in colisiones)
        {
            Portador enemigo = col.GetComponent<Portador>();
            if (enemigo != null && enemigo != objetivo)
            {
                enemigo.RecibirDa�o(da�o);
                Debug.Log($"Da�o AOE: {enemigo.nombre} recibe {da�o} de da�o.");
            }
        }

        ultimoUso = Time.time;
    }
}
