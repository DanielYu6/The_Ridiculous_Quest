using Oculus.Avatar2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SyncCups : MonoBehaviour
{
    public static SyncCups Instance;
    [SerializeField] private Cup[] redPlayerCups, redEnemyCups, bluePlayerCups, blueEnemyCups;

    const int maxHealth = 6;
    int redHealth = 6;
    int blueHealth = 6;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    public void RedCupEliminated(Cup cup)
    {
        int index = redEnemyCups.IndexOf(cup);

        cup.gameObject.SetActive(false);
        redPlayerCups[index].gameObject.SetActive(false);

        redHealth--;
        if (redHealth <= 0)
        {
            ResetGame();
        }
        
    }
    public void BlueCupEliminated(Cup cup)
    {
        int index = blueEnemyCups.IndexOf(cup);

        cup.gameObject.SetActive(false);
        bluePlayerCups[index].gameObject.SetActive(false);
        blueHealth--;
        if (blueHealth <= 0)
        {
            ResetGame();
        }
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        redHealth = blueHealth = maxHealth;
        for (int i = 0; i < 6; i++)
        {
            redPlayerCups[i].gameObject.SetActive(true);
            redEnemyCups[i].gameObject.SetActive(true);
            bluePlayerCups[i].gameObject.SetActive(true);
            blueEnemyCups[i].gameObject.SetActive(true);
        }
    }
}
