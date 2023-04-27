using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSellection : MonoBehaviour
{
	[SerializeField] private Transform _materialSellectionList;


	public void ToggleSellectionList()
	{
		if (_materialSellectionList == null)
		{
			Debug.LogError("_materialSellectionList  From ObjectsSellection is null");
			return;
		}

		_materialSellectionList.gameObject.SetActive(false);
	}

	public void ChangeColor(Material newColor)
	{
		GameManager.Instance.ChangeColor(newColor);
	}

	public void ShowSellectionList()
	{
		_materialSellectionList.gameObject.SetActive(true);
	}
}
