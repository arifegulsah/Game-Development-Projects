using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] public string sceneName;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            DontDestroyOnLoadScene.Instance.RemoveFromDontDestroyOnLoad();
            SceneManager.LoadScene(sceneName);
        }
    }
}
