using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
public class MapSceneLoader : MonoBehaviour
{
    [SerializeField] int SceneVariableForSelection;
    void Start()
    {
        
    }

    // Update is called once per frame
    public async void FirstCombatBeforeBag()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(2);
    }
    public async void QuestionCombatBeforeBag()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(5);
    }

    public async void NonQuestionMapCombatBeforeBag()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(8);
    }
    public async void AfterQuestionMapCombatBeforeBag()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(9);
    }
    public async void Selectionable()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(SceneVariableForSelection);
    }

    public async  void Bag()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(2);
    }
    public async void Map()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(1);
    }

    public async void EventSceneLeaveEvent()
    {
        ButtonSound();
        await Task.Delay(400);
        int prefNumber = 1;
        PlayerPrefs.SetInt("ListNumber", prefNumber);
    }

    public async void SecondMapLoader()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(4);
    }
    public async void ThirdFightBeforeBag()
    {
        ButtonSound();
        await Task.Delay(400);
        if (PlayerPrefs.HasKey("isFight"))
            SceneManager.LoadScene(9);
        else
            SceneManager.LoadScene(8);



    }
    public async void ThirdMapLoader()
    {
        ButtonSound();
        await Task.Delay(400);
        string path = "isFight";
        PlayerPrefs.SetString("isFight", path);
        SceneManager.LoadScene(7);
      
    }
    public async void FourthMapLoader()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(12);
        
    }
    public async void FourthFightBeforeBag()
    {
        ButtonSound();
        await Task.Delay(400);
        SceneManager.LoadScene(13);
       
    }

    public void Chest()
    {
        
    }
    void ButtonSound()
    {
        AudioManager.instance.SoundSfx(AudioManager.instance.audioClips[1]);
    }

}
