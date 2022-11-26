using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    private bool loaded;
    //[SerializeField] private Scene scene;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag.Equals("Player")) {
            Debug.Log(gameObject.name);

            Load();
        }
    }

    private void Load() {
        if(!loaded) {
            SceneManager.LoadScene(gameObject.name, LoadSceneMode.Additive);

            loaded = true;
        }
    }
}
