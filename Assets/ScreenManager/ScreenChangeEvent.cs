using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Screen Management/Screen Change Event")]
public class ScreenChangeEvent : ScriptableObject
{
    public UnityAction<ScreenType> OnScreenChange;

    public void RaiseEvent(ScreenType newScreen)
    {
        OnScreenChange?.Invoke(newScreen);
    }
}
