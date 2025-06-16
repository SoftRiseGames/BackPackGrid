using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadGamePlayMusicControl : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sahnedeki tüm MusicControl objelerini bul
        DontDestroyOnLoadGamePlayMusicControl[] musicControls = FindObjectsByType<DontDestroyOnLoadGamePlayMusicControl>(FindObjectsSortMode.None);

        foreach (var mc in musicControls)
        {
            // Eðer bu obje deðilse ve sahnedeki objeyse (taþýnmamýþsa), onu yok et
            if (mc != this && mc.gameObject.scene == scene)
            {
                Destroy(mc.gameObject);
            }
        }

        // Eðer sahne 0 ise yine kendini yok et (isteðe baðlý)
        if (scene.buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
