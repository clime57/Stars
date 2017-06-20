using UnityEngine;
using System.Collections;

public class FadeinMask : MonoBehaviour {

	void OnEnable()
	{
		AlphaTweener(0, 1, gameObject);
	}

	void OnDisable()
	{
		//AlphaTweener(1, 0);
		GameObject maskObject = (GameObject)Instantiate(gameObject, transform.localPosition, Quaternion.identity);
		maskObject.SetActive(false);
		maskObject.GetComponent<FadeinMask>().enabled = false;
		maskObject.SetActive(true);
		AlphaTweener(1, 0, maskObject, true);
	}

	void AlphaTweener(float s, float e, GameObject obj, bool state = false)
	{
		TweenAlpha alphaTween = obj.GetComponent<TweenAlpha>();
		if (alphaTween == null)
		{
			alphaTween = obj.AddComponent<TweenAlpha>();
		}
		alphaTween.from = s;
		alphaTween.to = e;
		alphaTween.ResetToBeginning();
		alphaTween.duration = 0.7f;
		if (state)
		{
			EventDelegate.Add(alphaTween.onFinished, () => { Destroy(obj); });
		}
		alphaTween.PlayForward();
	}
}
