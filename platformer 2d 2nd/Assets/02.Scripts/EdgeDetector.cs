using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeDetector : MonoBehaviour
{
    public bool isDetected
    {
        get => detectingRisingEdge || detectingFallingEdge;
    }
    public Vector2 climbPos
    {
        get => grabPos + new Vector2(_col.size.x / 2.0f * _col.gameObject.transform.lossyScale.x * _machineManager.direction, 0.0f);
    }
    public Vector2 grabPos
    {
        get => new Vector2(_rb.position.x + (_col.size.x / 2.0f * _col.gameObject.transform.lossyScale.x + 0.02f) * _machineManager.direction,
                           _rb.position.y + _col.size.y * _col.gameObject.transform.lossyScale.y);
    }
    public float topX => _rb.position.x + (_col.size.x / 2.0f * _col.gameObject.transform.lossyScale.x + 0.02f) * _machineManager.direction;
    public float topY => _rb.position.y + _col.size.y * _col.gameObject.transform.lossyScale.y + 0.025f;
    public float bottomX => _rb.position.x + (_col.size.x / 2.0f * _col.gameObject.transform.lossyScale.x + 0.02f) * _machineManager.direction;
    public float bottomY => _rb.position.y + _col.size.y * _col.gameObject.transform.lossyScale.y - 0.025f;

    public bool topOn, bottomOn;
    private bool detectingFallingEdge;
    private bool detectingRisingEdge;
    private StateMachineManager _machineManager;
    private Rigidbody2D _rb;
    private CapsuleCollider2D _col;
    [SerializeField] private LayerMask _groundLayer;
    private void Awake()
    {
        _machineManager = GetComponent<StateMachineManager>();
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<CapsuleCollider2D>();
    }
    private void Update()
    {
        topOn = Physics2D.OverlapCircle(new Vector2(topX,
                                                    topY),
                                                    0.015f,
                                                    _groundLayer);

        bottomOn = Physics2D.OverlapCircle(new Vector2(bottomX,
                                                       bottomY),
                                                       0.015f,
                                                       _groundLayer);




        // ¶³¾îÁú¶§ »ó½Â¿§Áö
        if (bottomOn &&
            topOn == false &&
            _rb.velocity.y < 0)
        {
            detectingRisingEdge = true;
            Debug.Log("»ó½Â¿§Áö°ËÃâ");
        }
        else
        {
            detectingRisingEdge = false;
        }

        // ¿Ã¶ó°¥¶§ ÇÏ°­¿§Áö
        //if (bottomOn == false &&
        //    topOn &&
        //    _rb.velocity.y >0)
        //{
        //    detectingFallingEdge = true;
        //}
        //else
        //{
        //    detectingFallingEdge = false;
        //}
    }

    private void OnDrawGizmosSelected()
    {
        if (_rb == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(new Vector3(topX, topY, 0f), 0.015f);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(bottomX, bottomY, 0f), 0.015f);
    }
}
