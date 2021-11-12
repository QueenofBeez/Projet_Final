using UnityEngine;
using UnityEngine.Events;

public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, 
    IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T> // Type, UnityEvent, UnityEventResponse
{
    [SerializeField] private E gameEvent;
    public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

    [SerializeField] private UER UnityEventResponse;

    private void OnEnable()
    {
        if(gameEvent == null) { return; }
        {
            GameEvent.RegisterListener(this);
        }
    }

    private void OnDisable()
    {
        if(gameEvent == null) { return; }

        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T item)
    {
        if(UnityEventResponse != null)
        {
            UnityEventResponse.Invoke(item);
        }
    }
}
