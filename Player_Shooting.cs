using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shooting : MonoBehaviour
{
    private Stats_Player stats_Player;
    private Player_WeaponHandler player_Weapon;
    public List<GameObject> bullets = new List<GameObject>();
    public List<Transform> firepointList = new List<Transform>();
    public float shootOffset;
    private float nextShootTime;
    public int manaUse = 1;
    private bool onShoot = false;

    private void Awake() {
        player_Weapon = GetComponent<Player_WeaponHandler>();
        stats_Player = GetComponent<Stats_Player>();
    }
    IEnumerator MultiShoot(){
        if(Time.time > nextShootTime && !onShoot){
            onShoot = true;
            Weapon currentWeapon = player_Weapon.GetWeapon();
            for(int i = 1; i <=currentWeapon.multiShootNumber; i++){
                int ammoUse = currentWeapon.ammoConsume;
                switch(currentWeapon.ammoType){
                    case Weapon.AmmoType.Stamina:{
                        if(stats_Player.TakeStamina(ammoUse)){
                            foreach(Transform firePoint in firepointList){
                                Quaternion spread = Quaternion.Euler(firePoint.rotation.eulerAngles + new Vector3(0, 0, currentWeapon.GetDispersion()));
                                int damage = currentWeapon.GetDamage();
                                GameObject bulletGO = Instantiate(currentWeapon.projection, firePoint.position, spread);
                                Bullet bullet = bulletGO.transform.GetComponentInChildren<Bullet>();
                                bullet.SetUp(transform, damage, currentWeapon.IsCrit(), currentWeapon.GetBulletSpeed(), currentWeapon.GetMaxDist());
                                Physics2D.IgnoreCollision(bullet.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
                            }

                        }  
                        break;
                    }
                    case Weapon.AmmoType.Mana:{
                        if(stats_Player.TakeMana(ammoUse)){ 
                            foreach(Transform firePoint in firepointList){
                                Quaternion spread = Quaternion.Euler(firePoint.rotation.eulerAngles + new Vector3(0, 0, currentWeapon.GetDispersion()));
                                int damage = currentWeapon.GetDamage();
                                GameObject bulletGO = Instantiate(currentWeapon.projection, firePoint.position, spread);
                                Bullet bullet = bulletGO.transform.GetComponentInChildren<Bullet>();
                                bullet.SetUp(transform, damage, currentWeapon.IsCrit(), currentWeapon.GetBulletSpeed(), currentWeapon.GetMaxDist());
                                Physics2D.IgnoreCollision(bullet.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
                            }
                        }  
                        break;
                    }
                    default: break;
                }
                yield return new WaitForSeconds(currentWeapon.multiShootOfsett);

            }
            nextShootTime = Time.time + currentWeapon.coldown;
            onShoot = false;         
        }

        
    }

    public void shoot(){
        switch(player_Weapon.GetWeapon().shootType){
            case Weapon.ShootType.single:{
                NormalShoot();
                break;
            }
            case Weapon.ShootType.multi:{
                StartCoroutine(MultiShoot());
                break;
            }
            default: break;
        }
    }
    private void NormalShoot(){
        if(Time.time > nextShootTime && !onShoot){
            onShoot = true;
            Weapon currentWeapon = player_Weapon.GetWeapon();
            int ammoUse = currentWeapon.ammoConsume;
            switch(currentWeapon.ammoType){
                case Weapon.AmmoType.Stamina:{
                    if(stats_Player.TakeStamina(ammoUse)){
                        foreach(Transform firePoint in firepointList){
                            Quaternion spread = Quaternion.Euler(firePoint.rotation.eulerAngles + new Vector3(0, 0, currentWeapon.GetDispersion()));
                            int damage = currentWeapon.GetDamage();
                            GameObject bulletGO = Instantiate(currentWeapon.projection, firePoint.position, spread);
                            Bullet bullet = bulletGO.transform.GetComponentInChildren<Bullet>();
                            bullet.SetUp(transform, damage, currentWeapon.IsCrit(), currentWeapon.GetBulletSpeed(), currentWeapon.GetMaxDist());
                            Physics2D.IgnoreCollision(bullet.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
                        }
                    }  
                    break;
                }
                case Weapon.AmmoType.Mana:{
                    if(stats_Player.TakeMana(ammoUse)){
                        foreach(Transform firePoint in firepointList){
                            Quaternion spread = Quaternion.Euler(firePoint.rotation.eulerAngles + new Vector3(0, 0, currentWeapon.GetDispersion()));
                            int damage = currentWeapon.GetDamage();
                            GameObject bulletGO = Instantiate(currentWeapon.projection, firePoint.position, spread);
                            Bullet bullet = bulletGO.transform.GetComponentInChildren<Bullet>();
                            bullet.SetUp(transform, damage, currentWeapon.IsCrit(), currentWeapon.GetBulletSpeed(), currentWeapon.GetMaxDist());
                            Physics2D.IgnoreCollision(bullet.transform.GetComponent<Collider2D>(), GetComponent<Collider2D>()); 
                        }
                    }  
                    break;
                }
                default: break;
            }
            nextShootTime = Time.time +  currentWeapon.coldown;
            onShoot = false;         
        }

    }
    public bool IsOnSoot(){
        return onShoot;
    }
    public void ChangeFirePoint(Transform parentTransform){
        firepointList.Clear();
        FirePoint[] firePoints = parentTransform.GetComponentsInChildren<FirePoint>();
        foreach(FirePoint firePoint in firePoints){
            firepointList.Add(firePoint.GetTransform());
        }
    }
}
