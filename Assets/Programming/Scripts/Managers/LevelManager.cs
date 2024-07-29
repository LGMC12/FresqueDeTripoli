using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public List<Level> levels = new List<Level>();

	[SerializeField] private int _currentLevelIndex = -1;
	[SerializeField] private static Level _currentLevel;

	[SerializeField] private Player _player;

	public Player Player { get { return _player; } }

	public static Level Level { get { return _currentLevel; } }

	public static LevelManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		Level.OnLevelClear += Level_OnLevelClear;
		Hud.OnTransitionDone += NextLevel;
		Hud.OnTutoDone += TutoDone;
	}

    private void TutoDone()
    {
        _currentLevel = Instantiate(levels[_currentLevelIndex]);
    }

    private void Start()
	{
		Hud.Instance.StartCoroutine(Hud.Instance.TutoStart());
		Player.PauseMovement();
	}

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.N)) Level_OnLevelClear();
    }

    private void Level_OnLevelClear()
	{
		Hud.Instance.StartCoroutine(Hud.Instance.LevelTransition());
	}

	private void NextLevel()
	{
		Destroy(_currentLevel.gameObject);
		++_currentLevelIndex;

        if (_currentLevelIndex < levels.Count)
        {
            _currentLevel = Instantiate(levels[_currentLevelIndex]);
            _player.ResetAnimator();
			Hud.Instance.StartCoroutine(Hud.Instance.LevelStart(_currentLevelIndex));
        }
		else Hud.Instance.StartCoroutine(Hud.Instance.EndGame());
	}

	private void OnDestroy()
	{
		Level.OnLevelClear -= Level_OnLevelClear;
		Hud.OnTransitionDone -= NextLevel;
        Hud.OnTutoDone -= TutoDone;
    }
}
