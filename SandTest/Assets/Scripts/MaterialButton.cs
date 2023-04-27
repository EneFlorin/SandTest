using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialButton : MonoBehaviour
{
    [SerializeField]private MaterialSellection _materialSellection;
	[SerializeField]private Material _myColor;


	public void OnButtonPressed()
	{
		if (_materialSellection == null)
		{
			Debug.LogError("_materialSellection is null");
			return;
		}

		_materialSellection.ChangeColor(_myColor);
	}
}
