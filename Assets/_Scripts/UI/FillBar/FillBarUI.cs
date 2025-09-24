using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LA.UI.FillBar
{
    public class FillBarUI : TemplateUI
    {
        [field: Header("FLAGS")]
        [field: SerializeField] public bool RightToLeftFill { get; private set; } = false;

        [field: Header("COMPONENTS")]
        [field: SerializeField] public Image BackgroundImage { get; private set; }

        [field: SerializeField] public Image FillImage { get; private set; }


        public virtual void Fill(float currentValue, float maxValue)
        {
            if (FillImage.enabled == false)
                FillImage.enabled = true;

            float fill = !RightToLeftFill ? currentValue / maxValue : 1 - (currentValue / maxValue);
            FillImage.fillAmount = fill;
        }


        public void SetColorScheme(Color fill = default, Color background = default)
        {
            FillImage.color = fill == default ? FillImage.color : fill;
            BackgroundImage.color = fill == default ? BackgroundImage.color : background;
        }
    }
}