using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    [System.Serializable]
    public class Screen
    {
        public ScreenType screenType;
        public GameObject screenObject;
    }

    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    [SerializeField] private Screen[] screens;

    private void OnEnable()
    {
        screenChangeEvent.OnScreenChange += HandleScreenChange;
    }

    private void OnDisable()
    {
        screenChangeEvent.OnScreenChange -= HandleScreenChange;
    }

    private void HandleScreenChange(ScreenType newScreen)
    {
        foreach (var screen in screens)
        {
            screen.screenObject.SetActive(screen.screenType == newScreen);
        }
    }
}
