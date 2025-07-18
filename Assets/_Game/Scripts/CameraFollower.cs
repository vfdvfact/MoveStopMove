using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform TF;
    public Transform playerTF;
    [SerializeField] Transform weaponShop;
    public int mode=1;
    [SerializeField] Vector3 originalRotation = new Vector3(20f, 0, 0);
    [SerializeField] Vector3 targetRotation = new Vector3(90f, 0f, 0f);
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 weaponShopOffset;
    [SerializeField] Vector3 skinShopOffset;
    private void Start()
    {
        TF = this.transform;
    }

    private void LateUpdate()
    {
        if (mode == 1)
        {
            if (playerTF != null)
            {
                TF.position = Vector3.Lerp(TF.position, playerTF.position + offset, Time.deltaTime * 5f);
            }
        }
        else if(mode == 2)
        {
            if (weaponShop != null)
            {
                TF.position = weaponShop.position + weaponShopOffset;
            }
        }
        else if(mode==3)
        {
            if (playerTF != null)
            {
                TF.position = Vector3.Lerp(TF.position, playerTF.position + skinShopOffset, Time.deltaTime * 5f);
            }
        }
    }
    public void ChangeCameraMode(int mod)
    {
        if (mod != 2)
        {
            TF.rotation = Quaternion.Euler(originalRotation);
        }        
        if (mod == 2)
        {
            TF.rotation = Quaternion.Euler(targetRotation);
        }
        mode = mod;
    }
}

