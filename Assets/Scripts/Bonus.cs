using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] BonusType bonusType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(20 * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            switch (bonusType)
            {
                case BonusType.Coin:
                    ScoreManager.instance.AddPoints(50);
                    break;
                case BonusType.Emerald:
                    ScoreManager.instance.AddPoints(100);
                    break;
                case BonusType.Ruby:
                    ScoreManager.instance.AddPoints(200);
                    break;
                case BonusType.Diamond:
                    ScoreManager.instance.AddPoints(500);
                    break;
                case BonusType.DarkDiamond:
                    ScoreManager.instance.AddPoints(500);
                    break;
            }
            Destroy(gameObject);
        }
    }
}

public enum BonusType
{
    Coin,
    Emerald,
    Ruby,
    Diamond,
    DarkDiamond,
}
