using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinGiverBlock : MonoBehaviour
{
    [SerializeField] UnityEvent coinCountUpdate = null;
    [SerializeField] GameObject coinPrefab = null;
    [SerializeField] int availableCoins = 2;
    void OnHit()
    {
        if(availableCoins > 0)
        {
            GiveCoin();
        }
        else
        {
            this.enabled = false;
            Destroy(this.gameObject, 1);
        }
    }

    public void GiveCoin()
    {
        coinCountUpdate.Invoke();
        availableCoins--;

        if(coinPrefab != null)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position + transform.up, transform.rotation);
        }
        else
        {
            Debug.LogWarning("Coin particle is missing in inspector", this.gameObject);
        }
    }
}
