using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    void Awake()
    {
        SaveData saveData = SaveSystem.LoadGame();
        int levelIdx = saveData != null ? saveData.level : 1;
        SceneManager.LoadScene(levelIdx);
    }
}
