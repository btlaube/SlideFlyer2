using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    //TODO make setter method for equipped projectileObject
    [SerializeField] private ProjectileObject projectileObject;
    [SerializeField] private GameObject projectile;

    //[SerializeField] private int startingAmmo;
    //[SerializeField] private int currentAmmo;
    [SerializeField] private PlayerAmmoInt currentAmmo;
    [SerializeField] private PlayerAmmoInt maxAmmo;
    private Transform bulletOrigin;

    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer = 0f;

    void Awake() {
        bulletOrigin = transform.GetChild(0);
    }

    void Start() {
        //playerAmmo.currentAmount
        currentAmmo.value = maxAmmo.value;
        fireTimer = Time.deltaTime;
    }

    void Update() {
        if (Input.touchCount > 1 ) {
            fireTimer += Time.deltaTime;
            if (currentAmmo.value > 0 && fireTimer >= fireRate) {
                Shoot();
                fireTimer = Time.deltaTime;
            }
            else if (currentAmmo.value <= 0) {
                //Change this to an Event
                //FindObjectOfType<GameManager>().EndGame();
            }
        }
    }

    void Shoot() {
        GameObject newProjectile = Instantiate(projectile, bulletOrigin.position, bulletOrigin.rotation, bulletOrigin);
        newProjectile.GetComponent<ProjectileBehavior>().projectileObject = projectileObject;
        currentAmmo.value--;
    }

}
