using UnityEngine;
using System.Collections;
namespace Stars
{
    public class DestroySelf : MonoBehaviour
    {
        public int _destroySelfType = 0;
        public float _destroySelfTime = 1.0f;
        void Start()
        {
            if (_destroySelfType == 0)
            {
                Invoke("InvokeDestroySelf", _destroySelfTime);
            }
            else
            {
                StartCoroutine("CoroutineDestroySelf");
            }
        }

        IEnumerator CoroutineDestroySelf()
        {
            yield return new WaitForSeconds(_destroySelfTime);
            GameObject.Destroy(gameObject);
        }

        public void InvokeDestroySelf()
        {
            GameObject.Destroy(gameObject);
        }
    }
}