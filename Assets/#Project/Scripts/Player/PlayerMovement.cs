using UnityEngine;
 using System.Collections;
  
public class PlayerMovement : MonoBehaviour
  
{
    void Update()
    {
        transform.Translate(
            Input.GetAxis("Horizontal") * 6.5f * Time.deltaTime,
            Input.GetAxis("Vertical") * 5f * Time.deltaTime, 0f);
    }
}