using TMFunds.UI;
using UnityEngine;

public class ScreenUI : MonoBehaviour
{
	[SerializeField] protected UIScreen _screen;

	protected virtual void Start()
	{
		_screen.OnPlay += Open;
		UIManager.SetCurrentScreen(_screen.Channel);
	}

	protected virtual void Open()
	{

	}

	protected virtual void OnDestroy()
	{
		_screen.OnPlay -= Open;
	}
}