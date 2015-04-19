using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.RectTransformUtility;

public class WeaponSelectScript : MonoBehaviour {

	public AttachWeaponScript.WeaponTypes weapon;

	// Use this for initialization
	void Start () {
	
	}
	
	public void Select ()
	{
		GameObject.Find("WeaponShop").GetComponent<WeaponShopScript>().Select(weapon);
		print(gameObject.name);	
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetMouseButtonDown(0))// && EventSystem.current.IsPointerOverGameObject())
		//{
		//	print(Input.mousePosition);
			/*if (RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
			{
				GameObject.Find("WeaponShop").GetComponent<WeaponShopScript>().Select(weapon);
				print(gameObject.name);	
			}*/
			
		//}
		
		/*RectTransform rect = GetComponent<RectTransform>();
		
		Vector2 topLeft = (Vector2) rect.position - (rect.sizeDelta / 2f);
		Rect soundRect = new Rect(topLeft.x, topLeft.y, rect.sizeDelta.x, rect.sizeDelta.y);
		
		bool tap = (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			&& !soundRect.Contains(Input.mousePosition);
			
		if (tap)
		{
			GameObject.Find("WeaponShop").GetComponent<WeaponShopScript>().Select(weapon);
			print(gameObject.name);	
		}*/
	}
}
