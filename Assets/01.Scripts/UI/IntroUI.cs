using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class IntroUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    private VisualElement _rootElement;

    private VisualElement _fade;
    private Button _startBtn;
    private Button _exitBtn;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _rootElement = _uiDocument.rootVisualElement;
        _fade = _rootElement.Q<VisualElement>("fade");
        _startBtn = _rootElement.Q<Button>("start-button");
        _startBtn = _rootElement.Q<Button>("exit-button");
        
        _startBtn.RegisterCallback<ClickEvent>(evt => 
        {
            StartCoroutine(FadeCo(() => { /*씬이동*/ }));
        });
        _exitBtn.RegisterCallback<ClickEvent>(etc => StartCoroutine(FadeCo(Application.Quit)));
    }

    private IEnumerator FadeCo(Action act)
    {
        _fade.AddToClassList("in");
        yield return new WaitForSeconds(0.5f);
        act?.Invoke();
    }
}
