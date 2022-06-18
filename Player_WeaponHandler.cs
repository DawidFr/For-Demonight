using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_WeaponHandler : MonoBehaviour
{
    public event EventHandler OnWeaponChanged;
    [SerializeField] Weapon firstWeapon;
    [SerializeField] Weapon secondWeapon;
    private Weapon currentWeapon;
    private bool haveFirstWeapon = true, haveSecondWeapon = true;
    private int currentWeaponNumber = 1;

    private void Awake() {
        currentWeapon = firstWeapon;
        if(OnWeaponChanged != null) OnWeaponChanged(this, EventArgs.Empty);
    }
    private void Start() {
        ClearSlot(2);
    }
    public void SwitchWeapon(){
        switch (currentWeaponNumber)
        {
            case 1:{
                if(haveSecondWeapon){
                    currentWeapon = secondWeapon;
                    currentWeaponNumber ++;
                }   
                break;
            }
            case 2:{
                if(haveFirstWeapon){
                    currentWeapon = firstWeapon;
                    currentWeaponNumber --;
                }
                break;
            }
            default:break;
        }
        if(OnWeaponChanged != null) OnWeaponChanged(this, EventArgs.Empty);

    }
    public Weapon GetWeapon(int weaponNumber = 0){
        switch (weaponNumber)
        {
            case 0: return currentWeapon;
            case 1: return firstWeapon;
            case 2: return secondWeapon;
            default: return null;
        }
    }
    public void ChangeWeapon(Weapon weapon){
        if(currentWeaponNumber == 1 && haveSecondWeapon){
            Weapon_ItemWorld.spawWeaponItem(transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f,1f)), firstWeapon);
            firstWeapon = weapon;
            currentWeapon = firstWeapon;            
            if(OnWeaponChanged != null) OnWeaponChanged(this, EventArgs.Empty);
        }
        else if(currentWeaponNumber == 1 && !haveSecondWeapon){
            secondWeapon = weapon;   
            haveSecondWeapon = true;
            SwitchWeapon();
        }
        else if(currentWeaponNumber == 2 && haveFirstWeapon){
            Weapon_ItemWorld.spawWeaponItem(transform.position + new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f,1f)), secondWeapon);
            secondWeapon = weapon;
            currentWeapon = secondWeapon;   
            if(OnWeaponChanged != null) OnWeaponChanged(this, EventArgs.Empty);
        }
        else if(currentWeaponNumber == 2 && !haveFirstWeapon){
            firstWeapon = weapon;
            haveFirstWeapon = true;
            SwitchWeapon();
        }

    }
    public void ClearSlot(int slotNumber){
        switch(slotNumber){
            case 1:{
                firstWeapon = null;
                haveFirstWeapon = false;
                break;
            }
            case 2:{
                secondWeapon = null;
                haveSecondWeapon = false;
                break;

            }
        }
        SwitchWeapon();
    }




}
