using UnityEngine;
using UnityEngine.UI;

public class SettingReset : MonoBehaviour
{
    public Slider animeSpeed;
    public void DefaultSetting()
    {
        animeSpeed.value = 50; ;
    }
}
