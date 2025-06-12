using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SecondMapLoader()
    {
        SceneManager.LoadScene(4);
    }
    public void ThirdMapLoader()
    {
        SceneManager.LoadScene(7);
    }
}
