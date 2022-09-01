using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool invincible { get; private set; }
    private Coroutine _invincibleCoroutine = null;
    public int damage;

    private int _hp;
    public int hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (value < 0)
                value = 0;

            _hpBar.value = (float)value / _hpMax;
            _hp = value;
        }
    }
    [SerializeField] private Slider _hpBar;
    [SerializeField] private int _hpMax;
    //private PlayerController _controller;
    private StateMachineManager _machineManager;
    private CapsuleCollider2D _col;
    public void Hurt(int damage)
    {
        if (invincible)
            return;

        hp -= damage;
        DamagePopUp.Create(transform.position + Vector3.up * _col.size.y * 0.7f,
                           damage,
                           gameObject.layer);

        if (_hp > 0)
        {
            //_controller.TryHurt();
            _machineManager.ChangeState(StateMachineManager.State.Hurt);
            InvincibleForSeconds(1.0f);
        }
        else
        {
            //_controller.TryDie();
            _machineManager.ChangeState(StateMachineManager.State.Die);
            invincible = true;
        }
    }

    public void InvincibleForSeconds(float seconds)
    {
        if (_invincibleCoroutine != null)
        {
            StopCoroutine(_invincibleCoroutine);
        }
        _invincibleCoroutine = StartCoroutine(E_InvincibleForSeconds(seconds));
    }
    IEnumerator E_InvincibleForSeconds(float seconds)
    {
        invincible = true;

        yield return new WaitForSeconds(seconds);
        invincible = false;
        _invincibleCoroutine = null;
    }
    private void Awake()
    {
        //_controller = GetComponent<PlayerController>();
        _machineManager = GetComponent<StateMachineManager>();
        _col = GetComponent<CapsuleCollider2D>();
        hp = _hpMax;
    }
}
