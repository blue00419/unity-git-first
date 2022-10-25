using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    [SerializeField]
    private BossHP bossHP;
    private Slider sliderHP;

    private void Awake(){
        sliderHP = GetComponent<Slider>();
    }

    private void Update(){
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;
    }
}
