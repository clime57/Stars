using UnityEngine;

public class RGGameState : GameState
{
	public override string update(float d)
	{
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.F10))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.F11))
        {
            AudioListener.volume = 0;
        }
#endif
		return "";
	}

	public override void start()
	{

	}
	public override  void over()
	{

	}
}

