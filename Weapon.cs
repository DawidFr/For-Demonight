using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class Weapon
{
    public enum WeaponSprite{
        shotgunRifle,
        smgRifle,
        sniperRifle,
        p90Rifle
    }
    public enum WeaponType{
        Range,
        Melee,
    }
    public enum AmmoType{
        Stamina,
        Mana,
    }
    public enum ShootType{
        single,
        multi,
    }
    public ShootType shootType;
    public AmmoType ammoType;
    public GameObject projection;
    public WeaponSprite weaponSprite;
    public WeaponType weaponType;
    public float multiShootOfsett;
    public int multiShootNumber;
    public float minBulletSpeed, maxBulletSpeed;
    private bool isCrit;   
    public int minDamage, maxDamage, ammoConsume;
    public float minMaxDist, maxMaxDist;
    public float coldown, critHitChange, minCritHitMultiplier, maxCritMultiplier , spread;
    public AmmoType GetAmmoType(){
        return ammoType;
    }
    public WeaponType GetWeaponType(){
        return weaponType;
    }
    public int GetDamage(){
        
        int damage =  UnityEngine.Random.Range(minDamage, maxDamage);
        isCrit = UnityEngine.Random.Range(0,100) < critHitChange * 100;
        if(isCrit) damage = (int) Mathf.Round(UnityEngine.Random.Range(minCritHitMultiplier, maxCritMultiplier) * damage);
        return damage;
    }

    public bool IsCrit(){
        bool isCrit = this.isCrit;
        this.isCrit = false;
        return isCrit;
    }
    public float GetDispersion(){
        return UnityEngine.Random.Range(-spread, spread);
    }
    public static Sprite GetSprite(WeaponSprite weapon){
        switch(weapon){
            case WeaponSprite.shotgunRifle: return ItemAsets.Instance.shotgunRifle;
            case WeaponSprite.smgRifle: return ItemAsets.Instance.smgRifle;
            case WeaponSprite.sniperRifle: return ItemAsets.Instance.sniperRifle;
            case WeaponSprite.p90Rifle: return ItemAsets.Instance.p90Rifle;
            default: return null;
        }
    }
    public static Transform GetPreferbs(WeaponSprite weapon){
        switch(weapon){
            case WeaponSprite.shotgunRifle: return ItemAsets.Instance.shotgunPf;
            case WeaponSprite.smgRifle: return ItemAsets.Instance.smgPf;
            case WeaponSprite.sniperRifle: return ItemAsets.Instance.sniperPf;
            case WeaponSprite.p90Rifle: return ItemAsets.Instance.p90Pf;
            default: return null;
        }
    }
    public float GetBulletSpeed(){
        return UnityEngine.Random.Range(minBulletSpeed, maxBulletSpeed);
    }
    public float GetMaxDist(){
        return UnityEngine.Random.Range(minMaxDist, maxMaxDist);
    }
    public static Weapon GetDefaultSetting(WeaponSprite weapon){
        switch(weapon){
            case WeaponSprite.shotgunRifle: return ItemAsets.Instance.defaultShotgun;
            case WeaponSprite.smgRifle: return ItemAsets.Instance.defaultSMG;
            case WeaponSprite.sniperRifle: return ItemAsets.Instance.defaultSniper;
            case WeaponSprite.p90Rifle: return ItemAsets.Instance.defaultP90;
            default: return null;
        }
    }
    

}
