using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Enemy _Enemy;
    private Image _ProgressBar;
    private Graphic _ProgressBarColor;
   
    private float fill = 0;

    private void Start()
    {
        _Enemy = transform.root.GetComponent<Enemy>();
        _ProgressBar = GetComponent<Image>();
        _ProgressBarColor = GetComponent<Graphic>();
        InvokeRepeating("HealthBars", 0, 0.25f);
    }

    private Color ColorBarS(float param)
    {
        if (param < 0.5f) return new Color(1, param * 2, 0, 1);
        else return new Color(1 / (param * 2), 1, 0, 1);
    }

    private void HealthBars()
    {
        fill = _Enemy.Health / _Enemy.MaXHealth;
        _ProgressBar.fillAmount = fill;
        _ProgressBarColor.color = ColorBarS(fill);
    }
}
