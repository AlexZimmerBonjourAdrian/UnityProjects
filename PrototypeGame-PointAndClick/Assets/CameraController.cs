using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    private static CameraController Inst;
    private static Camera mainCamera;
    public static CameraController inst
    { 
         get { 
                if( Inst == null )
                {
                 GameObject Camera = new GameObject("MainCamera");
                 Inst = Camera.AddComponent<CameraController>();
                 mainCamera = Camera.AddComponent<Camera>();
                 mainCamera.orthographic = true;
                }
               
              return Inst;
        }
    }

    public bool init()
    {

        return false;
        
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update

    // Update is called once per frame
   
}
