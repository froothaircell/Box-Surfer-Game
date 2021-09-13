using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class YellowBoxSensor : MonoBehaviour
{
    [SerializeField]
    private UnityEvent yellowEvent = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            yellowEvent.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            //Destroy(other);
            yellowEvent.Invoke();
            Destroy(gameObject);
        }
    }
}
