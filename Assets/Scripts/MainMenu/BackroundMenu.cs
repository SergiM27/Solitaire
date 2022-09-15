using UnityEngine;

public class BackroundMenu : MonoBehaviour
{
    public int aleatorio;

    ObjectPooler objectPooler;

    public float tiempoCreacion = 0.12f;

    private GameObject plano;


    void Start()
    {
        aleatorio = 0;
        objectPooler = ObjectPooler.Instance;
        InvokeRepeating("planeSpawn", 0f, tiempoCreacion);
    }

    public void planeSpawn()
    {
        aleatorio = Random.Range(1, 15);
        GameObject spawn = GameObject.Find("Spawn" + aleatorio.ToString());
        plano = objectPooler.GetPooledObject("Cards", spawn.transform.position, spawn.transform.rotation);
    }
}



