using UnityEngine;
using TMPro;

public class DebugInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text txtLeft = default;
    [SerializeField] private TMP_Text txtRight = default;

    public void SetText(string left, string right)
    {
        txtLeft.SetText(left, false);
        txtRight.SetText(right, false);
    }
}