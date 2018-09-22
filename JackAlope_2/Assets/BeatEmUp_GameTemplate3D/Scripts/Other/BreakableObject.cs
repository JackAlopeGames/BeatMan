using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BreakableObject : MonoBehaviour, IDamagable<DamageObject> {

	public string hitSFX = "";

	[Header ("Gameobject Destroyed")]
	public GameObject destroyedGO;
	public bool orientToImpactDir;

	[Header ("Spawn an item")]
	public GameObject spawnItem;
	public float spawnChance = 100;

	[Space(10)]
	public bool destroyOnHit;

    public GameObject Coin;
	void Start(){
		gameObject.layer = LayerMask.NameToLayer("DestroyableObject");
	}

	//this object was Hit
	public void Hit(DamageObject DO){

		//play hit sfx
		if (hitSFX != "") {
			GlobalAudioPlayer.PlaySFXAtPosition (hitSFX, transform.position);
		}

		//spawn destroyed gameobject version
		if (destroyedGO != null) {
			GameObject BrokenGO = GameObject.Instantiate (destroyedGO);
			BrokenGO.transform.position = new Vector3( transform.position.x, BrokenGO.transform.position.y,transform.position.z);

			//chance direction based on the impact direction
			if (orientToImpactDir && DO.inflictor != null) {
				float dir = Mathf.Sign(DO.inflictor.transform.position.x - transform.position.x);
				BrokenGO.transform.rotation = Quaternion.LookRotation(Vector3.forward * dir);
			}

            float maxDis = this.Coin.GetComponent<Coin>().maxdistance;
            GameObject coin1 = Instantiate(this.Coin, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity);
            GameObject coin2 = Instantiate(this.Coin, new Vector3(this.transform.position.x, this.transform.position.y + 2, this.transform.position.z), Quaternion.identity);
            coin1.GetComponent<Coin>().Maxdistance = 2;
            coin2.GetComponent<Coin>().Maxdistance = 2;
            coin1.GetComponent<Rigidbody>().AddForce(new Vector3(-1, 2, -1) * 2, ForceMode.Impulse);
            coin2.GetComponent<Rigidbody>().AddForce(new Vector3(1, 2, 1) * 2, ForceMode.Impulse);
        }

		//spawn an item
		if (spawnItem != null) {
			if (Random.Range (0, 100) < spawnChance) {
				GameObject item = GameObject.Instantiate (spawnItem);
				item.transform.position = transform.position;

                //add up force to object
                if (spawnItem.gameObject.name != "HealthPickup_Banana")
                {
                    item.GetComponent<Rigidbody>().velocity = Vector3.up * 8f;
                }
                else
                {
                    item.GetComponent<Rigidbody>().velocity = Vector3.up * 8f + Vector3.left * 2f + Vector3.forward * 2;
                }
			}
		}

		//destroy 
		if (destroyOnHit) {
			Destroy(gameObject);
		}
	}
}