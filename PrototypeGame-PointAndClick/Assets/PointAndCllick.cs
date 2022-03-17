using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;

namespace GameEngine
{ 
public class PointAndCllick : MonoBehaviour
{
        
        [SerializeField]
        private Camera mainCamera;
        
        private void Awake()
        {
            CameraController.inst.init();
        }

        // Start is called before the first frame update

        Vector3 MouseLocation()
        {
            return Input.mousePosition; 
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (Input.GetButtonDown("Fire")&& collision.gameObject.tag== "Interactive")
            {
                
            }
        }

    }
    
    

}