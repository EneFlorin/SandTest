using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
	private DraggableObject _parent => GetComponentInParent<DraggableObject>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Snap")
		{

			var snap = other.gameObject.GetComponent<SnapPoint>();

			if (snap != null)
			{
				var obj = snap.GetParent();

				if (obj.IsMoving || _parent.IsMoving)
				{
					return;
				}

				AlignTo(snap);
				SnapTo(snap);

				_parent.SnapToOtherObject(obj.transform);
			}
		}
	}

	private void SnapTo(SnapPoint other)
	{
		var offset = _parent.transform.position - transform.position;
		var newPosition = other.transform.position + offset;
		_parent.transform.position = newPosition;
	}

	private void AlignTo(SnapPoint other)
	{
		var rotationOffset = transform.rotation.eulerAngles.y - _parent.transform.rotation.eulerAngles.y;
		_parent.transform.rotation = other.transform.rotation;
	}

	public DraggableObject GetParent() => _parent;
}
