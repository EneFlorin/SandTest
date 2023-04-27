using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DraggableObject : MonoBehaviour
{
	private Vector3 _screenPoint;
	private Vector3 _offset;
	private Rigidbody _rigidbody;
	private const float _dragTimerThreshold = 0.3f;
	private float _dragTimer = 0;
	private Material _myMaterial;
	private string _name;
	private string _materialName;
	private bool _isMoving = false;

	public bool IsMoving { get { return _isMoving; } set { } }

	private void Awake()
	{
		LoadAndSave.AllObjects.Add(this);
	}

	private void Start()
	{
		_rigidbody = GetComponent<Rigidbody>();
	}

	void OnMouseDown()
	{
		_isMoving = true;

		_screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

		_offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z));
	}

	void OnMouseDrag()
	{
		_dragTimer += Time.deltaTime;

		Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);

		Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + _offset;
		transform.position = curPosition;

		_rigidbody.isKinematic = true;
	}

	private void OnMouseUp()
	{
		_rigidbody.isKinematic = false;
		UnSnap();
		if (_dragTimerThreshold > _dragTimer)
		{
			GameManager.Instance.SellectNewObject(this);
		}
		_dragTimer = 0;

		_isMoving = false;
	}

	public void AllocateName(string name)
	{
		_name = name;
	}

	public string GetName() => _name;
	public string GetMaterialName() => _materialName;

	public void ChangeColor(Material newColor)
	{
		GetComponent<MeshRenderer>().material = newColor;
		_materialName = newColor.name;
	}

	private void OnDestroy()
	{
		LoadAndSave.AllObjects.Remove(this);
	}

	public void SnapToOtherObject(Transform parent)
	{
		if (_rigidbody != null)
		{
			_rigidbody.isKinematic = true;
		}
		transform.parent = parent;
	}

	public void UnSnap()
	{
		transform.parent = null;
		_rigidbody.isKinematic = false;
	}
}
