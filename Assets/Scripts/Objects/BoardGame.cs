using UnityEngine;

public class BoardGame : MonoBehaviour, ActivityInterface
{
    [SerializeField] GameObject dice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dice.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public KeyCode key => KeyCode.F;

    public void OnActivity()
    {       

        dice.SetActive(true);
    }
}
