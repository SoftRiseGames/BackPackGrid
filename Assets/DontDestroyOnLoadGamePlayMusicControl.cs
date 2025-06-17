using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoadGamePlayMusicControl : MonoBehaviour
{
    private static DontDestroyOnLoadGamePlayMusicControl instance;

    private void Awake()
    {
        // Eðer zaten bir instance varsa ve bu deðilse, yok et
        if (instance != null && instance != this)
        {
            if (gameObject.name == "GamePlayMusic")
            {
                Destroy(gameObject);
            }
            return;
        }

        // Ýlk instance atanýyor
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Menü sahnesi kontrolü için sahne yüklenme olayýna abone ol
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Eðer ana menü sahnesi ise (örneðin build index 0), kendini yok et
        if (scene.buildIndex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        // Olaydan çýkmayý unutma, aksi halde null reference olabilir
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
