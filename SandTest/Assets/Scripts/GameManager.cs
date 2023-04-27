using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Singleton 
	public static GameManager Instance { get; private set; }
	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}

		MakeMaterialDictionary();
		MakePrefabsDictionary();
	}

	#endregion

	[SerializeField] private MaterialSellection _materialSellection;
	[SerializeField] private List<Material> _allMaterials = new List<Material>();
	[SerializeField] private List<DraggableObject> _allPrefabs = new List<DraggableObject>();

	private DraggableObject _sellectedObject;
	private Dictionary<string, Material> _materialDictionary = new Dictionary<string, Material>();
	private Dictionary<string, DraggableObject> _prefabDictionary = new Dictionary<string, DraggableObject>();

	private void MakeMaterialDictionary()
	{
		if (_allMaterials.Count == 0)
		{
			Debug.LogError("No materials in list   _allMaterials");
			return;
		}

		_allMaterials.ForEach((x) => { _materialDictionary.Add(x.name, x); });
	}

	private void MakePrefabsDictionary()
	{
		if (_allPrefabs.Count == 0)
		{
			Debug.LogError("No prefabs in list   _allPrefabs");
			return;
		}

		_allPrefabs.ForEach((x) => { _prefabDictionary.Add(x.name, x); });
	}

	public void SellectNewObject(DraggableObject newObject)
	{
		_sellectedObject = newObject;

		if (_materialSellection == null)
		{
			Debug.LogError("_materialSellection is null");
			return;
		}

		_materialSellection.ShowSellectionList();
	}

	public void DeselectObject()
	{
		_sellectedObject = null;
	}

	public void ChangeColor(Material newColor)
	{
		if (_sellectedObject != null)
		{
			_sellectedObject.ChangeColor(newColor);
		}
	}

	public Material TryGetMaterial(string name)
	{
		if (name == null)
		{
			return null;
		}

		_materialDictionary.TryGetValue(name, out var material);

		if (material == null)
		{
			Debug.LogError("No material with that name");
			return null;
		}

		return material;
	}

	public DraggableObject TryGetPrefab(string name)
	{
		_prefabDictionary.TryGetValue(name, out var obj);

		if (obj == null)
		{
			Debug.LogError("No material with that name");
			return null;
		}

		return obj;
	}
}
