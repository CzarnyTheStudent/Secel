using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float Health = 1f;
    public TextMeshProUGUI DeathCountTxt;
    public float DeathCount;
    public DeathCounter deathCounter;

    private float OneHealth = 1f;

    private void Start()
    {
        DeathCount = deathCounter.deathCountSave;
        DeathCountTxt.SetText("Deaths: " + DeathCount);
    }

    private void Update()
    {
        if (Health < OneHealth)
        {
            Debug.Log("U died");
            Add1Death();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
           // deathCounter?.deathCountSave
        }
    }

    public void Add1Death()
    {
        deathCounter.Add1ToDeaths();
    }

    public void TakeDamage()
    {
        Health--;
    }

    private void OnDisable()
    {
       DeathCount = 0;
    }

}
