
using UnityEngine;

[System.Serializable]
public class ObjectData
{
	public string PrefabName;
	public string MaterialName;
	public float[] Position;
	public float[] Rotation;

	public ObjectData(DraggableObject obj)
	{
		PrefabName = obj.GetName();
		MaterialName = obj.GetMaterialName();

		Vector3 objPos = obj.transform.position;
		Quaternion objRot = obj.transform.rotation;

		Position = new float[]
		{
			objPos.x,objPos.y,objPos.z
		};

		Rotation = new float[]
		{
			objRot.x,objRot.y,objRot.z,objRot.w
		};
	}
}
