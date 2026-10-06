using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundAudioSource;
    [SerializeField] private AudioSource effectAudioSource;

    [SerializeField] private AudioClip backgroundClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip coinClip;

    public const string BG_KEY = "Vol_Background";
    public const string EFFECT_KEY = "Vol_Effect";
    public const float DEFAULT_BG = 0.7f;
    public const float DEFAULT_EFFECT = 1f;

    public float BackgroundVolume { get; private set; }
    public float EffectVolume { get; private set; }

    private void Awake()
    {
        // Đọc âm lượng đã lưu (mặc định nhạc nền 0.7, hiệu ứng 1)
        BackgroundVolume = PlayerPrefs.GetFloat(BG_KEY, DEFAULT_BG);
        EffectVolume = PlayerPrefs.GetFloat(EFFECT_KEY, DEFAULT_EFFECT);
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        backgroundAudioSource.clip = backgroundClip;
        backgroundAudioSource.loop = true;
        backgroundAudioSource.volume = BackgroundVolume;
        backgroundAudioSource.Play();
    }

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

    private void OnDestroy() => PlayerPrefs.Save();
}