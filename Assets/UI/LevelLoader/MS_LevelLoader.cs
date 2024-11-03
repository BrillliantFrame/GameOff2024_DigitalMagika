using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MS_LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.PageUp)) //Testing purposes > Make bound to level entry/exit
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        //Example: Load scene with by name
        //SceneManager.LoadScene("Scene B"); 

        //Example: Load next scene by build order
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play Animation
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
