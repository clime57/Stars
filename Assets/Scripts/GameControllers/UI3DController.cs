using UnityEngine;
using System.Collections;
namespace Stars
{
    public class UI3DController : MonoBehaviour
    {
        public GameObject _avatar;
        public Camera _camera;
        RenderTexture rtt = null;
        Quaternion _defaultRotation;
        void Awake()
        {
            if (rtt == null)
            {
                rtt = new RenderTexture(512, 512, 24);
                _camera.targetTexture = rtt;
                _camera.ResetAspect();
            }
            _defaultRotation = _avatar.transform.localRotation;
        }

        void OnDestroy()
        {
            rtt = null;
        }
        // Use this for initialization
        void Start()
        {
            //testload();
        }

        void Update()
        {
            if (rtt != null)
            {

                rtt.DiscardContents();
            }
        }
        public void rotate_y(float angle)
        {
            _avatar.transform.Rotate(0, angle, 0);
        }

        void backToDefault()
        {
            _avatar.transform.localRotation = _defaultRotation;
        }

        public RenderTexture getTexture()
        {
            return rtt;
        }

        public RenderTexture setTextureSize(int x, int y)
        {
            rtt = null;
            rtt = new RenderTexture(x, y, 24);
            _camera.targetTexture = rtt;
            _camera.ResetAspect();
            return rtt;
        }

        public void setLayer(int layer)
        {
            Renderer[] renderer = _avatar.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in renderer)
            {
                r.gameObject.layer = layer;
            }
        }
    }

}