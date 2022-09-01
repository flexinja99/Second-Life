using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopUpAssets : MonoBehaviour
{
    private static DamagePopUpAssets _instance;
    public static DamagePopUpAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<DamagePopUpAssets>("Assets/DamagePopUpAssets"));
            return _instance;
        }
    }

    [SerializeField] private DamagePopUp _damagePopUp1; // 플레이어가 피격시
    [SerializeField] private DamagePopUp _damagePopUp2; // 몬스터가 피격시

    public DamagePopUp GetDamagePopUp(int layer)
    {
        DamagePopUp tmpDamagePopUp = _damagePopUp1;
        if (layer == LayerMask.NameToLayer("Enemy"))
        {
            tmpDamagePopUp = _damagePopUp2;
        }
        return tmpDamagePopUp;
    }
}
