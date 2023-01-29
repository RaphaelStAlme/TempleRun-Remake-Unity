using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] Material[] bonusMaterials;
    [SerializeField] BonusType bonusType;

    // Start is called before the first frame update
    void Start()
    {
        SelectRandomBonusType();
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
            Debug.Log(bonusType);
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

    private void SelectRandomBonusType()
    {
        var renderer = gameObject.GetComponent<Renderer>();
        int selectedBonus = Random.Range(0, bonusMaterials.Length - 1);
        renderer.material = bonusMaterials[selectedBonus];

        switch (selectedBonus)
        {
            case 1:
                bonusType = BonusType.Coin;
                break;
            case 2:
                bonusType = BonusType.Emerald;
                break;
            case 3:
                bonusType = BonusType.Ruby;
                break;
            case 4:
                bonusType = BonusType.Diamond;
                break;
            case 5:
                bonusType = BonusType.DarkDiamond;
                break;
        }
    }
}

    public enum BonusType
    {
        Void,
        Coin,
        Emerald,
        Ruby,
        Diamond,
        DarkDiamond,
    }
