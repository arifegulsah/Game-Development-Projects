using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    [SerializeField] public GameObject[] objects; //that i dont want to lose //arrayde tutuyoruz tek tek attachlemiyoruz. çünkü dontdestroy methodu tek bir sefere mahsus çalýþýr kabul ediliyor.

    public static DontDestroyOnLoadScene Instance; //instance vermez isek sahne geçiþleri yaþanýrken objeler duplicate edilir ve giderek coðalýr.

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("Sahnede birden fazla instance of DontDestroyOnLoadScene var!!!");
            return;
        }
        else
        {
            Instance = this;
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        foreach(var element in objects)
        {
            SceneManager.MoveGameObjectToScene(element, SceneManager.GetActiveScene());
        }
    }
}
