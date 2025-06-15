using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonOpenerEvent : MonoBehaviour
{
    public Button AfterButtons;
    public Button WayFinderButtonLockerButton;
    public bool WayFinder;

    public GameObject Chess;

    public bool isChess;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("isPressed" + gameObject.name))
        {
            Debug.Log("isPressed" + gameObject.name);
            StarterSetup();
        }
    }
    void Start()
    {
            
    }

  
    void Update()
    {
        
    }
    public void ButtonUnlocker()
    {
        Debug.Log("girdi");
        gameObject.GetComponent<Button>().interactable = false;
        
        if (WayFinder)
            WayFinderButtonLockerButton.interactable = false;

        if (isChess)
            Chess.SetActive(true);

        string Path = "isPressed";
        PlayerPrefs.SetString("isPressed"+gameObject.name, Path);
        Debug.Log("isPressed" + gameObject.name);


       if(!PlayerPrefs.HasKey("isPressed" + AfterButtons.gameObject.name))
            AfterButtons.interactable = true;

    }

    void StarterSetup()
    {
        Debug.Log("girdi");
        gameObject.GetComponent<Button>().interactable = false;

        if (WayFinder)
            WayFinderButtonLockerButton.interactable = false;

        string Path = "isPressed";
        PlayerPrefs.SetString("isPressed" + gameObject.name, Path);
        Debug.Log("isPressed" + gameObject.name);


        if (!PlayerPrefs.HasKey("isPressed" + AfterButtons.gameObject.name))
            AfterButtons.interactable = true;
    }
}
