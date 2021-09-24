using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent RunEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent WinOrDeathEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent RestartEvent = new UnityEvent();

    private int state = 0;

    // Start is called before the first frame update
    private void Start()
    {
        state = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        switch(state)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }

    public void Run()
    {
        if(state == 0)
        {
            state = 1;
            RunEvent.Invoke();
        }
    }

    public void WinOrDeath()
    {
        if(state == 1)
        {
            state = 2;
            WinOrDeathEvent.Invoke();
        }
    }

    public void Restart()
    {
        if(state == 2)
        {
            state = 0;
            RestartEvent.Invoke();
        }
    }
}
