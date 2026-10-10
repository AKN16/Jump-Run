using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Scene")]
    [SerializeField] private string gameSceneName = "Game"; // đổi đúng tên scene game của bạn

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [Header("Background group")]
    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip gameStartClip;
    [SerializeField] private AudioClip gameOverClip;

    [Header("Effect group")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip buttonClip;

    public const string BG_KEY = "Vol_Background";
    public const string EFFECT_KEY = "Vol_Effect";
    public const float DEFAULT_BG = 0.7f;
    public const float DEFAULT_EFFECT = 1f;

    public float BackgroundVolume { get; private set; }
    public float EffectVolume { get; private set; }

    private bool isGameOver;

    private void Awake()
    {
        // Singleton: nếu đã có bản khác (quay lại menu) thì huỷ bản mới
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        BackgroundVolume = PlayerPrefs.GetFloat(BG_KEY, DEFAULT_BG);
        EffectVolume = PlayerPrefs.GetFloat(EFFECT_KEY, DEFAULT_EFFECT);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Chạy mỗi khi load scene (kể cả scene đầu tiên)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterAllButtons();

        if (scene.name == gameSceneName)
        {
            PlayGameStart();
        }
        else
        {
            // Scene menu: không phát start/over
            isGameOver = false;
            StopAllCoroutines();
            backgroundAudioSource.Stop();
        }
    }

    // ----- Nút bấm: gắn âm thanh cho mọi Button trong scene hiện tại -----
    private void RegisterAllButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button btn in buttons)
        {
            btn.onClick.RemoveListener(PlayButtonSound); // tránh gắn trùng
            btn.onClick.AddListener(PlayButtonSound);
        }
    }

    public void PlayButtonSound()
    {
        if (buttonClip != null)
            effectAudioSource.PlayOneShot(buttonClip, EffectVolume);
    }

    // ----- Game start: phát 1 lần, xong thì chạy nhạc nền -----
    public void PlayGameStart()
    {
        isGameOver = false;
        StopAllCoroutines();
        StartCoroutine(GameStartRoutine());
    }

    private IEnumerator GameStartRoutine()
    {
        if (gameStartClip != null)
        {
            backgroundAudioSource.Stop();
            backgroundAudioSource.clip = gameStartClip;
            backgroundAudioSource.loop = false;
            backgroundAudioSource.volume = BackgroundVolume;
            backgroundAudioSource.Play();

            yield return new WaitForSeconds(gameStartClip.length);
        }

        if (!isGameOver)
            PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.volume = BackgroundVolume;
        backgroundAudioSource.Play();
    }

    // ----- Game over: dừng nhạc nền, phát 1 lần -----
    public void PlayGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        StopAllCoroutines();
        backgroundAudioSource.Stop();
        backgroundAudioSource.clip = gameOverClip;
        backgroundAudioSource.loop = false;
        backgroundAudioSource.volume = BackgroundVolume;
        backgroundAudioSource.Play();
    }

    // ----- Effect -----
    public void PlayJumpSound()
    {
        effectAudioSource.PlayOneShot(jumpClip, EffectVolume);
    }

    public void PlayCoinSound()
    {
        effectAudioSource.PlayOneShot(coinClip, EffectVolume);
    }

    // ----- Gọi từ Slider trong Setting -----
    public void SetBackgroundVolume(float value)
    {
        BackgroundVolume = value;
        backgroundAudioSource.volume = value;
        PlayerPrefs.SetFloat(BG_KEY, value);
    }

    public void SetEffectVolume(float value)
    {
        EffectVolume = value;
        PlayerPrefs.SetFloat(EFFECT_KEY, value);
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            PlayerPrefs.Save();
        }
    }
}