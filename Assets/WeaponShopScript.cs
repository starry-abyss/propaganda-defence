using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponShopScript : MonoBehaviour {

	AttachWeaponScript.WeaponTypes currentWeapon;
	
	bool shoppingDiscovered = false;
	bool lieGeneratorDiscovered = false;
	bool brothelDiscovered = false;

	public AttachWeaponScript.WeaponTypes SelectedWeapon()
	{
		return currentWeapon;
		//return AttachWeaponScript.WeaponTypes.LieGenerator;
	}

	// Use this for initialization
	void Start () {
		/*Text[] shopPrices = GetComponentsInChildren<Text>();
		for (int i = 0; i != shopPrices.Length; ++i)
		{
			shopPrices[i].text = "$" + AttachWeaponScript.weaponPrices[i+1].ToString() + "K";
		}*/
		for (int i = 0; i < transform.childCount; ++i)
		{
			transform.GetChild(i).Find("Price").GetComponent<Text>().text 
				= "$" + AttachWeaponScript.weaponPrices[i+1].ToString() + "K";
		}
		
		currentWeapon = AttachWeaponScript.WeaponTypes.None;
	}
	
	/*static public void SelectStatic(AttachWeaponScript.WeaponTypes weapon)
	{
		GameObject.Find("WeaponShop").GetComponent<WeaponShopScript>().Select(weapon);
	}*/
	
	public void Select(AttachWeaponScript.WeaponTypes weapon)
	{
		if (!shoppingDiscovered && (weapon == AttachWeaponScript.WeaponTypes.ShoppingCenter))
			return;
		if (!lieGeneratorDiscovered && (weapon == AttachWeaponScript.WeaponTypes.LieGenerator))
			return;
		if (!brothelDiscovered && (weapon == AttachWeaponScript.WeaponTypes.Brothel))
			return;
			
		if (AttachWeaponScript.weaponPrices[(int) weapon] > StatsScript.money)
			return;
			
		currentWeapon = weapon;
	}
	
	// Update is called once per frame
	void Update () {
		;
		
		shoppingDiscovered = WaveManagerScript.currentWave >= 2;
		lieGeneratorDiscovered = WaveManagerScript.currentWave >= 4;
		brothelDiscovered = WaveManagerScript.currentWave >= 6;
		
		int availableCount = 0;
	
		for (int i = 0; i < transform.childCount; ++i)
		{
			bool expensive = AttachWeaponScript.weaponPrices[i+1] > StatsScript.money;
			transform.GetChild(i).Find("Expensive").GetComponent<Image>().enabled = expensive;
				
			if (expensive && ((int) currentWeapon == i+1))
				currentWeapon = AttachWeaponScript.WeaponTypes.None;
				
			transform.GetChild(i).Find("Selected").GetComponent<Image>().enabled = (i+1 == (int) currentWeapon);
				
			bool discovered = true;
			if (i+1 == (int) AttachWeaponScript.WeaponTypes.Brothel)
				discovered = brothelDiscovered;
			if (i+1 == (int) AttachWeaponScript.WeaponTypes.LieGenerator)
				discovered = lieGeneratorDiscovered;
			if (i+1 == (int) AttachWeaponScript.WeaponTypes.ShoppingCenter)
				discovered = shoppingDiscovered;
				
			transform.GetChild(i).Find("Undiscovered").GetComponent<Image>().enabled = !discovered;
			transform.GetChild(i).Find("Undiscovered").GetComponentInChildren<Text>().enabled = !discovered;
			
			if (!expensive && discovered) ++availableCount;
		}
		
		if (availableCount == 1) Select(AttachWeaponScript.WeaponTypes.DrinkSpot);
	}
}
