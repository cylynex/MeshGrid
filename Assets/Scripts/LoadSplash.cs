using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSplash : MonoBehaviour {

    void Start() {
        StartCoroutine("LoadGame");
    }


    IEnumerator LoadGame() {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("Main");
    }
}
