using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager: MonoBehaviour
{
    GameObject shadow;
    GameObject protagonist;
    PolygonCollider2D myCollider;

    int currentSceneIndex;
    public int nextSceneIndex;
    
    private void Start()
    {
        shadow = GameObject.Find("Shadow");
        protagonist = GameObject.Find("Protagonist");

        myCollider = GetComponent<PolygonCollider2D>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {

    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(1.25f);
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
