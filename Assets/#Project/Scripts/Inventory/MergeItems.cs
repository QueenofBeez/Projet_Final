using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeItems : MonoBehaviour
{
    public GameObject swappedInto;
 
    public Transform blankspace;
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //print("hello");
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(1))
        {
 
            if (hit.transform.position == transform.position)
            {
                print("hello");
                Instantiate(swappedInto, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
 
 
    }
}
