using UnityEngine;
using UnityEngine.UI;

// Chỉ lo phần slider âm lượng. Mở/đóng panel do script Menu xử lý.
// Ở scene Menu: để trống ô Audio Manager (âm lượng được lưu vào PlayerPrefs).
public class SettingsUI : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager; // có thể để trống
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider effectSlider;

    private void Start()
    {
        // Hiện giá trị đã lưu lên slider
        backgroundSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat(AudioManager.BG_KEY, AudioManager.DEFAULT_BG));
        effectSlider.SetValueWithoutNotify(
            PlayerPrefs.GetFloat(AudioManager.EFFECT_KEY, AudioManager.DEFAULT_EFFECT));

        backgroundSlider.onValueChanged.AddListener(OnBackgroundChanged);
        effectSlider.onValueChanged.AddListener(OnEffectChanged);
    }

    private void OnBackgroundChanged(float value)
    {
        PlayerPrefs.SetFloat(AudioManager.BG_KEY, value);
        if (audioManager != null) audioManager.SetBackgroundVolume(value);
    }

    private void OnEffectChanged(float value)
    {
        PlayerPrefs.SetFloat(AudioManager.EFFECT_KEY, value);
        if (audioManager != null) audioManager.SetEffectVolume(value);
    }

    private void OnDestroy() => PlayerPrefs.Save();
}