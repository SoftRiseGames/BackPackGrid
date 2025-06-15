using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonOpenerEvent : MonoBehaviour
{
    public Button AfterButtons;
    public Button WayFinderButtonLockerButton;
    public bool WayFinder;

    
    
    void Start()
    {
        if (PlayerPrefs.HasKey("isPressed" + gameObject.name))
            ButtonUnlocker(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonUnlocker()
    {
        if (WayFinder)
            WayFinderButtonLockerButton.interactable = false;

        string Path = "isPressed";
        PlayerPrefs.SetString("isPressed"+gameObject.name, Path);
        gameObject.GetComponent<Button>().interactable = false;
        AfterButtons.interactable = true;
    }
}
