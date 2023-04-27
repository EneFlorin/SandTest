using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSellection : MonoBehaviour
{
	[SerializeField] private Transform _objectsSellectionList;
	[SerializeField] private DraggableObject _cubePrefab;
	[SerializeField] private DraggableObject _cylinderPrefab;
	[SerializeField] private DraggableObject _cuboidPrefab;

	public void ToggleSellectionList()
	{
		if (_objectsSellectionList == null)
		{
			Debug.LogError("_objectsSellectionList  From ObjectsSellection is null");
			return;
		}

		_objectsSellectionList.gameObject.SetActive(!_objectsSellectionList.gameObject.activeInHierarchy);
	}

	public void CubeCreate()
	{
		CreateAnObject(_cubePrefab);
	}

	public void CylinderCreate()
	{
		CreateAnObject(_cylinderPrefab);
	}

	public void CuboidCreate()
	{
		CreateAnObject(_cuboidPrefab);
	}

	private void CreateAnObject(DraggableObject obj)
	{
		if (obj == null)
		{
			Debug.LogError("The prefab is null");
			return;
		}

		var _newObject = Instantiate<DraggableObject>(obj , Vector3.one , Quaternion.identity);
		_newObject.AllocateName(obj.name);
		ToggleSellectionList();
	}
}
