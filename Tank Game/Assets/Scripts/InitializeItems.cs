using UnityEngine;
using System.Collections;
using Assets.Classes.Items;
using Assets.Classes.Effects;

public class InitializeItems : MonoBehaviour {

    public GameObject ItemPrefab;
    public GameObject RespawningItemPrefab;

	void Awake()
    {
        GameObject item1 = (GameObject)Instantiate(RespawningItemPrefab, new Vector3(-21f, 0f, 21f), Quaternion.identity);
        GameObject item2 = (GameObject)Instantiate(ItemPrefab, new Vector3(21f, 0f, 21f), Quaternion.identity);
        GameObject item3 = (GameObject)Instantiate(ItemPrefab, new Vector3(-21f, 0f, -21f), Quaternion.identity);
        GameObject item4 = (GameObject)Instantiate(RespawningItemPrefab, new Vector3(21f, 0f, -21f), Quaternion.identity);

        item1.GetComponent<RespawningItem>().AddLongEffect(new HealthLongEffect()
        {
            healthModifier = 1f,
            intensity = 0.1f,
            duration = 5f,
            dummyMonoBehaviourForStartingCoroutines = this
        });
        item1.GetComponent<RespawningItem>().RespawnTime = 30f;
        item2.GetComponent<Item>().AddLongEffect(new HealthLongEffect()
        {
            healthModifier = 1f,
            intensity = 0.1f,
            duration = 5f,
            dummyMonoBehaviourForStartingCoroutines = this
        });
        item3.GetComponent<Item>().AddLongEffect(new HealthLongEffect()
        {
            healthModifier = 1f,
            intensity = 0.1f,
            duration = 5f,
            dummyMonoBehaviourForStartingCoroutines = this
        });
        item4.GetComponent<RespawningItem>().AddLongEffect(new HealthLongEffect()
        {
            healthModifier = 1f,
            intensity = 0.1f,
            duration = 5f,
            dummyMonoBehaviourForStartingCoroutines = this
        });
        item4.GetComponent<RespawningItem>().RespawnTime = 30f;
    }
}
