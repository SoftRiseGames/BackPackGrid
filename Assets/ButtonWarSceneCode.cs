using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonWarSceneCode : MonoBehaviour
{
    public List<int> SceneLoader;
    public int ListSceneSelect;

    void Start()
    {
        if (PlayerPrefs.HasKey("ListNumber"))
            ListSceneSelect = PlayerPrefs.GetInt("ListNumber");
        else
            ListSceneSelect = 0;
    }

    public void ButtonCountChange()
    {
        if (ListSceneSelect < SceneLoader.Count)
        {
            int prefNumber = ListSceneSelect + 1;
            PlayerPrefs.SetInt("ListNumber", prefNumber);
        }
        SceneManager.LoadScene(SceneLoader[ListSceneSelect]);
       
        
    }
}
