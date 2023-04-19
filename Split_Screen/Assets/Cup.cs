using Oculus.Interaction.Samples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BallDetected();
            other.GetComponentInParent<RespawnOnDrop>().Respawn();
        }
    }
    void BallDetected()
    {
        if (isRed)
        {
            SyncCups.Instance.RedCupEliminated(this);
        } else
        {
            SyncCups.Instance.BlueCupEliminated(this);
        }
    }
}
