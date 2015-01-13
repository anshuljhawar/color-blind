using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// This is just an example, normaly the values would be something meaningful for the application
/// </summary>

public class ToggleValue : MonoBehaviour
{
    public enum ToggleValues { Easy, Medium, Hard }

    [SerializeField]
    private ToggleValues value = ToggleValues.Easy;
    public ToggleValues Value { get { return value; } set { this.value = value; } }

    private void Start()
    {
        GetComponentInChildren<Text>().text = value.ToString();
    }
}
