using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MasterScript : MonoBehaviour
{
    public delegate void NextLevelEvent();
    public static event NextLevelEvent OnNextLevel;

    public static Vector3 spawnPos;
    public static Quaternion spawnRot;

    public List<string> levels;
    private int currentLevel = -1;

    public static int orbs = 0;
    public static int orbsCollected = 0;

    private void OnEnable()
    {
        Win.OnLevelFinish += NextLevel;
        Death.OnDeath += Lose;
    }

    private void OnDisable()
    {
        Win.OnLevelFinish -= NextLevel;
        Death.OnDeath -= Lose;
    }

    void Lose()
    {
        Debug.Log("Death");
    }

    void NextLevel()
    {
        orbs = 0;

        Timer.NextLevel();

        SceneManager.LoadScene("Level" + (currentLevel+1), LoadSceneMode.Additive);
        if(currentLevel != -1)
            if (SceneManager.GetSceneByName("Level"+currentLevel).isLoaded)
                SceneManager.UnloadSceneAsync("Level" + currentLevel);
        currentLevel++;

        if (OnNextLevel != null)
            OnNextLevel();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene("UI_MainMenu", LoadSceneMode.Additive);
        //return;

        var deb = DebugStart();


        if (deb)
        {
            orbs = 0;
            var s = SceneManager.GetSceneByName("Level" + currentLevel);
            StartCoroutine(LoadSceneWait(s.name));
            //SceneManager.LoadScene(s.name, LoadSceneMode.Additive);
            Debug.Log("Debug orbs " + orbs);
        }
        else
        {
            currentLevel = 0;
            NextLevel();
        }
        SceneManager.LoadScene("Player", LoadSceneMode.Additive);
    }

    bool DebugStart()
    {
        bool hasDebug = false;

        var scenes = SceneManager.GetAllScenes();

        string highLevel = "";

        foreach (var s in scenes)
        {
            var sub = s.name.Substring(0,5);

            if (string.Compare(sub, "Level", true) == 0)
                highLevel = s.name.Substring(5);

            if (s.name != "Master")
                StartCoroutine(UnloadSceneWait(s.name));

        }

        int loadedLevel = -1;

        if (!string.IsNullOrEmpty(highLevel))
        {

            int.TryParse(highLevel, out loadedLevel);

            if (loadedLevel != -1)
                currentLevel = loadedLevel;
        }

        return loadedLevel != -1;
    
    }

    IEnumerator LoadSceneWait(string scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene,LoadSceneMode.Additive);
        yield return ao;
    }
    IEnumerator UnloadSceneWait(string scene)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(scene);
        yield return ao;
    }

}
