using UnityEngine;
using UnityEngine.UIElements;

public class InGameUI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _rootElement;
    private VisualElement _sliderValue;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _sliderValue = _rootElement.Q<VisualElement>("value");
    }

    private void Update()
    {
        UpdateHP();
    }

    public void UpdateHP()
    {
        _sliderValue.style.flexBasis = new StyleLength(Length.Percent(Mathf.Lerp(0, 100, GameManager.Instance.Player.CurrentHP / (float)GameManager.Instance.Player.MaxHP)));
    }
}
