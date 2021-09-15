using UnityEngine;
using UnityEngine.Events;

// Checks for player and adds a yellow box accordingly via event
public class YellowBoxSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent yellowEvent = new UnityEvent();
    [SerializeField]
    private UnityEvent yellowAltEvent = new UnityEvent();

    private GameObject myParent;

    private void Start()
    {
        myParent = transform.parent.gameObject;
        Debug.Log(myParent.name);
    }

    // Update is called once per frame
    void Update()
    {
        // Only used for debugging purposes
        /*if(Input.GetButtonDown("Jump"))
        {
            yellowEvent.Invoke();
        }
        if(Input.GetButtonDown("Fire2"))
        {
            yellowAltEvent.Invoke();
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug.Log("We're in the collision");
        if (collision.collider.CompareTag("Player") 
            || collision.collider.CompareTag("Player Base") 
            || collision.collider.CompareTag("Character"))
        {
            yellowEvent.Invoke();
            // Debug.Log("the name of the parent is: " + myParent.name);
            Destroy(myParent);
        }
    }
}
