using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public List<Level> levels = new List<Level>();

	[SerializeField] public int _startLevelIndex = 0;
	public static int StartIndex = 0;
	private int _currentLevelIndex = 0;
	[SerializeField] private static Level _currentLevel;

	[SerializeField] private Player _player;

	public Player Player { get { return _player; } }

	public static Level Level { get { return _currentLevel; } }

	public static LevelManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
		
		Level.OnLevelClear += Level_OnLevelClear;
		
		LevelTransition.OnLevelTransitionAnimDone += NextLevel;
		LevelTransition.OnFirstLevelStartAnimDone += TutoDone;
		
		_currentLevelIndex = _startLevelIndex = StartIndex;
	}

    private void TutoDone()
    {
	    UIManager.tutoFinished = true;
        _currentLevel = Instantiate(levels[_currentLevelIndex]);
    }

    private void Start()
	{
		LevelTransition.Instance.StartCoroutine(LevelTransition.Instance.FirstLevelStartAnim(_startLevelIndex));
		Player.PauseMovement();
	}

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.N)) Level_OnLevelClear();
    }

    private void Level_OnLevelClear()
	{
		LevelTransition.Instance.StartCoroutine(LevelTransition.Instance.LevelTransitionAnim());
	}

	private void NextLevel()
	{
		Destroy(_currentLevel.gameObject);
		++_currentLevelIndex;

        if (_currentLevelIndex < levels.Count)
        {
            _currentLevel = Instantiate(levels[_currentLevelIndex]);
            _player.ResetAnimator();
            LevelTransition.Instance.StartCoroutine(LevelTransition.Instance.LevelStartAnim(_currentLevelIndex));
        }
        else
        {
	        EndGame.Instance.StartCoroutine(EndGame.Instance.EndGameAnim());
        }
	}

	private void OnDestroy()
	{
		Level.OnLevelClear -= Level_OnLevelClear;
		
		LevelTransition.OnLevelTransitionAnimDone -= NextLevel;
		LevelTransition.OnFirstLevelStartAnimDone -= TutoDone;
    }
}
