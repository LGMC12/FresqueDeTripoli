using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPixel : Enemy
{
    [Header("Animations")]
    [SerializeField] private string _appearTrigger;
    [SerializeField] private string _disappearTrigger;

    private float _directionTimer;
    [SerializeField] private float _directionDuration;

    private float _teleportTimer;
    [SerializeField] private float _teleportDuration;

    private Zone _currentZone;

    private bool _isTeleporting = false;

    private void Start()
    {
        foreach (Zone lZone in LevelManager.Level.ZonesList)
        {
            if (lZone.CheckEnemyEntrance(this))
            {
                _currentZone = lZone;
                break;
            }
        }
    }

    public void AppearingDone()
    {
        _isTeleporting = false;
    }

    public void DisappearingDone()
    {
        foreach (Zone lZone in LevelManager.Level.ZonesList)
        {
            if (lZone != _currentZone && lZone.CheckEnemyEntrance(this))
            {
                _currentZone.EnemyTeleporting(this);
                _currentZone = lZone;
                break;
            }
        }

        transform.position = _currentZone.AppearPoint.position;
        m_animator.SetTrigger(_appearTrigger);
    }

    protected override void Move()
    {
        m_rb.velocity = m_direction * m_speed;
    }

    private void Update()
    {
        if (!_isTeleporting)
        {
            _directionTimer += Time.deltaTime;
            _teleportTimer += Time.deltaTime;

            if (_directionTimer >= _directionDuration)
            {
                _directionTimer -= _directionDuration;
                RandomizeDirection();
            }

            if (_teleportTimer >= _teleportDuration)
            {
                _teleportTimer -= _teleportDuration;
                Teleport();
                return;
            }

            Move();
        }
    }

    private void Teleport()
    {
        _isTeleporting = true;
        m_rb.velocity = Vector2.zero;

        m_animator.SetTrigger(_disappearTrigger);
    }

    private void RandomizeDirection()
    {
        int lRandomX = Random.Range(-1, 2);
        int lRandomY = Random.Range(-1, 2);

        if (lRandomX != 0) { lRandomY = 0; }

        m_direction = new Vector2(lRandomX, lRandomY);
    }
}
