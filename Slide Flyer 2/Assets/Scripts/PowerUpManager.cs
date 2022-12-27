using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour {
    
    #region Singleton
    public static PowerUpManager instance;

    void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public delegate void OnPowerUpChanged();
    public OnPowerUpChanged onPowerUpChangedCallback;

    public List<PowerUpObject> powerUps = new List<PowerUpObject>();

    public void Add(PowerUpObject powerUp) {
        if (powerUps.Contains(powerUp)) {
            powerUp.runtimeCount++;
        }
        else {
            powerUps.Add(powerUp);
        }

        if (onPowerUpChangedCallback != null) {
            onPowerUpChangedCallback.Invoke();
        }
    }

    public void Remove(PowerUpObject powerUp) {
        if (powerUp.runtimeCount > 1) {
            powerUp.runtimeCount--;
        }
        else {
            powerUps.Remove(powerUp);
        }

        if (onPowerUpChangedCallback != null) {
            onPowerUpChangedCallback.Invoke();
        }
    }

}