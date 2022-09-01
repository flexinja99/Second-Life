using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopUp : MonoBehaviour
{
    private TMP_Text _textMeshPro;
    private float _disappearTimer = 0.5f;
    private float _disappearSpeed = 2.0f;
    private float _moveSpeedY = 0.5f;
    private Color _color;

    public static DamagePopUp Create(Vector3 pos, int damage, int layer)
    {
        DamagePopUp tmpDamagePopUp = Instantiate(DamagePopUpAssets.instance.GetDamagePopUp(layer), pos, Quaternion.identity);
        tmpDamagePopUp.SetUp(damage);
        return tmpDamagePopUp;
    }

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        transform.position += Vector3.up * _moveSpeedY * Time.deltaTime;

        if (_disappearTimer < 0.0f)
        {
            _color.a -= _disappearSpeed * Time.deltaTime;
            _textMeshPro.color = _color;
            if (_color.a < 0)
                Destroy(gameObject);
        }
        else
        {
            _disappearTimer -= Time.deltaTime;
        }
    }

    private void SetUp(int damage)
    {
        _textMeshPro.SetText(damage.ToString());
    }
}