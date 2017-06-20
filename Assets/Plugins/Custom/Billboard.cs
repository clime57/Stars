using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour {

    public Camera myCamera;
	Quaternion direction=new Quaternion();
	
	// Use this for initialization
	void Start () {
        direction.x = transform.localRotation.x;
        direction.y = transform.localRotation.y;
        direction.z = transform.localRotation.z;
        direction.w = transform.localRotation.w;
		myCamera = Camera.current;
		Camera[] cameras = Camera.allCameras;
		foreach(Camera cam in cameras){
			if(cam.tag == "MainCamera"){
				myCamera = cam;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
        Camera cam=null;
        if (myCamera != null)
        {
            cam = myCamera;
        }
        else
        {
            cam = Camera.current;
            if (!cam)
                return;
        }
        transform.rotation = cam.transform.rotation * direction;
		//transform.Rotate(0,90,0);
	}
	
	public void resetCamera(){
		myCamera = Camera.current;
		Camera[] cameras = Camera.allCameras;
		foreach(Camera cam in cameras){
			if(cam.tag == "MainCamera"){
				myCamera = cam;
				break;
			}
		}
	}
	
	public void setCamera(Camera cam){
		myCamera = cam;
	}
}
