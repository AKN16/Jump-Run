using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    public enum Type { Background, Effect }
    [SerializeField] private Type type;

    private void Start()
    {
        Slider slider = GetComponent<Slider>();
        var am = AudioManager.Instance;

        // Hiện đúng giá trị đã lưu, rồi mới gắn sự kiện
        slider.value = type == Type.Background ? am.BackgroundVolume : am.EffectVolume;
        slider.onValueChanged.AddListener(v =>
        {
            if (type == Type.Background) am.SetBackgroundVolume(v);
            else am.SetEffectVolume(v);
        });
    }
}