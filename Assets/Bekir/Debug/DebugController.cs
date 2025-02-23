using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class DebugController : MonoBehaviour
{
    private List<string> _errorMessages = new List<string>(){
        "o ne öyle",
        "o komutu degis",
        "ben anlamadim o komutu",
        "oyle bir komut mu varmis",
        "bende oyle bir bilgi yok",
        "onu bulursan banada haber et"
    };
    private bool _showConsole;
    private bool _showHelp;

    private string _input;

    public static DebugCommand HELP;
    public static DebugCommand<string> SPAWN_CARD;
    public static DebugCommand<string, int> SPAWN_CARD_X;
    public static DebugCommand<int> ADD_MONEY;
    public static DebugCommand<int> SET_ROUND;

    public List<object> CommandList;
    [SerializeField] private CartHandler _cartHandler;
    private string _errorMessage;


    private void Awake()
    {
        HELP = new DebugCommand("help", "Learn Cheats", "help", (() =>
        {
            _showHelp = true;
        }));

        SPAWN_CARD = new DebugCommand<string>("add ", "Kart Spawnlar", "add <isim>", (string cardName) =>
        {
            _cartHandler.SpawnCart(cardName);
        });
        SPAWN_CARD_X = new DebugCommand<string, int>("add ", "girilen sayi kadar kart spawnlar", "add <isim> <sayi>", (string cardName, int count) =>
        {
            for (int i = 0; i < count; i++)
            {
                _cartHandler.SpawnCart(cardName);
            }
        });
        CommandList = new List<object>()
        {
            SPAWN_CARD_X,
            SPAWN_CARD,
            HELP,
        };
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _showConsole = !_showConsole;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleInput();
        }
    }
    private bool _wrongCommand = false;
    private void HandleInput()
    {
        if (_input == "") return;
        _errorMessage = $"{_errorMessages[Random.Range(0, _errorMessages.Count)]}";
        string[] properties = _input.Split(' ');

        for (int i = 0; i < CommandList.Count; i++)
        {
            DebugCommandBase commandBase = CommandList[i] as DebugCommandBase;

            // Komut ID'si input'un BAŞLANGICINDA mı?
            if (_input.StartsWith(commandBase.commandId))
            {
                _wrongCommand = false;
                // Komut ID'sinin uzunluğunu çıkararak parametreleri al
                string[] parameters = _input.Substring(commandBase.commandId.Length).Split(' ');

                // DebugCommand<string, int> (iki parametreli) için
                if (CommandList[i] is DebugCommand<string, int> cmdX)
                {
                    if (parameters.Length >= 2 && int.TryParse(parameters[1], out int count))
                    {
                        cmdX.Invoke(parameters[0], count);
                        _input = "";
                        return;
                    }
                }
                // DebugCommand<string> (tek parametreli) için
                else if (CommandList[i] is DebugCommand<string> cmd)
                {
                    cmd.Invoke(parameters[0]);
                    _input = "";
                    return;
                }
                // DebugCommand (parametresiz) için
                else if (CommandList[i] is DebugCommand cmdBasic)
                {
                    cmdBasic.Invoke();
                    _input = "";
                    return;
                }
            }
            else
            {
                _wrongCommand = true;
            }
        }
    }
    private Vector2 _scroll;

    private void OnGUI()
    {
        if (!_showConsole)
        {
            return;
        }
        GUI.color = Color.white;
        float y = 0;

        if (_showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");

            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * CommandList.Count);

            _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), _scroll, viewport);

            for (int i = 0; i < CommandList.Count; i++)
            {
                DebugCommandBase commandBase = CommandList[i] as DebugCommandBase;

                string label = $"{commandBase.commandFormat} - {commandBase.commandDescription}";

                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);

                GUI.Label(labelRect, label);
            }

            GUI.EndScrollView();

            y = +100;
        }
        if (_wrongCommand)
        {
            GUI.color = Color.red;
            string label = $"{_errorMessage}";
            Rect labelRect = new Rect(5, Screen.height - 20, Screen.width - 100, 20);
            GUI.Label(labelRect, label);
        }
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        if (GUI.Button(new Rect(Screen.width - 50f, y + 30, 50, 30), "Spawn"))
        {
            Debug.Log("Spawned");
            HandleInput();
        }
        GUI.SetNextControlName("TextField");
        _input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), _input);
        GUI.backgroundColor = new Color(0, 0, 0, 0);
    }
}
