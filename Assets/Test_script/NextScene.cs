using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private int next;
    void Start()
    {
        next = SceneManager.GetActiveScene().buildIndex + 1;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player")
        {
            Debug.Log("bitch");
            SceneManager.LoadScene(next);
        }
    }
}
