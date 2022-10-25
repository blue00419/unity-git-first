using UnityEngine;
using TMPro;

public class PlayerBoomCountViewer : MonoBehaviour
{
    [SerializeField]
    private Weapon weapon;
    private TextMeshProUGUI textBoomCount;

    private void Awake(){
        textBoomCount = GetComponent<TextMeshProUGUI>();
    }

    private void Update(){
        textBoomCount.text = "x " + weapon.BoomCount;
    }
}
