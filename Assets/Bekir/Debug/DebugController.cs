using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DebugController : MonoBehaviour
{
    private bool showConsole;
    private bool showHelp;

    private string input;

    public static DebugCommand HELP;
    public static DebugCommand<string> SPAWN_CARD;
    public static DebugCommand<int> ADD_MONEY;
    public static DebugCommand<int> SET_ROUND;

    public List<object> commandList;
    [SerializeField] private CartHandler _cartHandler;
    private void Awake()
    {
        HELP = new DebugCommand("help", "Learn Cheats", "help", (() =>
        {
            showHelp = true;
        }));
        
        SPAWN_CARD = new DebugCommand<string>("add", "Add card to the hand", "add", (string cardName) =>
        {
            Debug.Log("CardSpawned");
            _cartHandler.SpawnCart(cardName);
        });

        commandList = new List<object>()
        {
            SPAWN_CARD,
            HELP
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            showConsole = !showConsole;
        }
    }

    private void HandleInput()
    {
        if(input == "")return;
        string[] properities = input.Split(' ');
        
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

            if (input.Contains(commandBase.commandId))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }
                else if (commandList[i] as DebugCommand<string> != null)
                {
                    (commandList[i] as DebugCommand<string>).Invoke(properities[1]);
                }
            }
        }
    }

    private Vector2 _scroll;
    
    private void OnGUI()
    {
        if (!showConsole) {return;}
        
        float y = 0;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y,Screen.width,100),"");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), _scroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                string label = $"{commandBase.commandFormat} - {commandBase.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i,viewport.width - 100,20);
                
                GUI.Label(labelRect,label);
            }
            
            GUI.EndScrollView();
            
            y =+ 100;
        }
        
        GUI.Box(new Rect(0, y ,Screen.width,30),"");

        
        if (GUI.Button(new Rect(Screen.width - 50f, y + 30, 50, 30), "Spawn"))
        {
            Debug.Log("Spawned");
            HandleInput();
        }
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);

        GUI.backgroundColor = new Color(0, 0, 0, 0);
    }
}
