using UnityEngine;

public class ScreenChanger : MonoBehaviour
{
    [SerializeField] private ScreenChangeEvent screenChangeEvent;
    [SerializeField] private ScreenType targetScreen;

    public void ChangeScreen()
    {
        screenChangeEvent.RaiseEvent(targetScreen);
    }
}
