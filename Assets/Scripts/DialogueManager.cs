using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI DialogueTextPoint;
    [SerializeField] List<string> Dialogues;

    [SerializeField] Button Button1;
    [SerializeField] Button Button2;

    [SerializeField] List<Sprite> list;
    void Start()
    {
        DialogueTextPoint.text = Dialogues[0];
    }

    public async void HelpEvent()
    {
        Button1.interactable = false;
        Button2.interactable = false;
        DialogueTextPoint.text = Dialogues[1];
        await Task.Delay(2000);
        DialogueTextPoint.text = Dialogues[2];
        await Task.Delay(2000);
        SceneManager.LoadScene(2);

    }
    public async void LeaveEvent()
    {
        Button1.interactable = false;
        Button2.interactable = false;
        int prefNumber = 1;
        PlayerPrefs.SetInt("ListNumber", prefNumber);
        await Task.Delay(2000);
        SceneManager.LoadScene(1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
