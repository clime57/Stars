using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Stars;
public class SampleView : MonoBehaviour {
    void OnClick()
    {
        Messenger.Broadcast<float>(SampleEvents.MSG1, 3);
    }
}
