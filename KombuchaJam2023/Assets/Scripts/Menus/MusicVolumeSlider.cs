using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Slider))]
public class MusicVolumeSlider : MonoBehaviour
{
    #region Self-Initiated References

    Slider _mySlider;
    Slider MySlider
    {
        get
        {
            if (_mySlider == null)
                _mySlider = GetComponent<Slider>();

            return _mySlider;
        }
    }

    #endregion

    [SerializeField] TextMeshProUGUI myText;

    float oldSliderValue;

    private void Awake()
    {
        MySlider.value = MusicManager.instance.musicVolumeMultiplier;
        myText.text = (int)(MySlider.value * 100) + "%";
        oldSliderValue = MySlider.value;
    }

    private void Update()
    {
        if (MySlider.value == oldSliderValue)
            return;

        oldSliderValue = MySlider.value;

        MusicManager.instance.SetTrackVolumeMultiplier(MySlider.value);

        myText.text = (int)(MySlider.value * 100) + "%";
    }
}
