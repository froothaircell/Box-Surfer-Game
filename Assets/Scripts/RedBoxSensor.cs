using UnityEngine;
using UnityEngine.Events;

public class RedBoxSensor : MonoBehaviour
{
    [SerializeField]
    UnityEvent DeathEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.parent = null;
        }
        else if(collision.collider.CompareTag("Player Base") || collision.collider.CompareTag("Character"))
        {
            Debug.Log("OnCollisionEnter ran here");
            DeathEvent.Invoke();
        }    

    }
}
