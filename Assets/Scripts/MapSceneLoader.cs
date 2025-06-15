using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapSceneLoader : MonoBehaviour
{
    [SerializeField] int SceneVariableForSelection;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void FirstCombatBeforeBag()
    {
        SceneManager.LoadScene(2);
    }
    public void QuestionCombatBeforeBag()
    {
        SceneManager.LoadScene(5);
    }

    public void NonQuestionMapCombatBeforeBag()
    {
        SceneManager.LoadScene(8);
    }
    public void AfterQuestionMapCombatBeforeBag()
    {
        SceneManager.LoadScene(9);
    }
    public void Selectionable()
    {
        SceneManager.LoadScene(SceneVariableForSelection);
    }

    public void Bag()
    {
        SceneManager.LoadScene(2);
    }
    public void Map()
    {
        SceneManager.LoadScene(1);
    }

    public void EventSceneLeaveEvent()
    {
        int prefNumber = 1;
        PlayerPrefs.SetInt("ListNumber", prefNumber);
    }

    public void SecondMapLoader()
    {
        SceneManager.LoadScene(4);
    }
    public void ThirdFightBeforeBag()
    {
        if (PlayerPrefs.HasKey("isFight"))
            SceneManager.LoadScene(9);
        else
            SceneManager.LoadScene(8);


    }
    public void ThirdMapLoader()
    {
        string path = "isFight";
        PlayerPrefs.SetString("isFight", path);
        SceneManager.LoadScene(7);
    }
    public void FourthMapLoader()
    {
        SceneManager.LoadScene(12);
    }
    public void FourthFightBeforeBag()
    {
        SceneManager.LoadScene(13);
    }

    public void Chest()
    {
        
    }

}
