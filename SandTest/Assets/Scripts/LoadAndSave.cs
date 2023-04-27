using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LoadAndSave : MonoBehaviour
{
	public static List<DraggableObject> AllObjects = new List<DraggableObject>();

	private const string OBJECT_PATH = "/object";
	private const string OBJECT_COUNT = "/object.count";

	private void Start()
	{
		LoadObjects();
	}

	private void OnApplicationQuit()
	{
		SaveObjects();
	}

	public void SaveObjects()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + OBJECT_PATH;
		string countPath = Application.persistentDataPath + OBJECT_COUNT;

		FileStream countStream = new FileStream(countPath, FileMode.Create);
		formatter.Serialize(countStream, AllObjects.Count);

		for (int i = 0; i < AllObjects.Count; i++)
		{
			FileStream stream = new FileStream(path + i, FileMode.Create);
			ObjectData data = new ObjectData(AllObjects[i]);

			formatter.Serialize(stream, data);
			stream.Close();
		}
	}

	public void LoadObjects()
	{
		BinaryFormatter formatter = new BinaryFormatter();
		string path = Application.persistentDataPath + OBJECT_PATH;
		string countPath = Application.persistentDataPath + OBJECT_COUNT;
		int objectCount = 0;

		if (File.Exists(countPath))
		{
			FileStream countStream = new FileStream(countPath, FileMode.Open);

			objectCount = (int)formatter.Deserialize(countStream);
			countStream.Close();
		}
		else
		{
			Debug.LogError("The path is empty - " + countPath);
		}

		for (int i = 0; i < objectCount; i++)
		{
			if (File.Exists(path + i))
			{
				FileStream stream = new FileStream(path + i, FileMode.Open);
				ObjectData data = formatter.Deserialize(stream) as ObjectData;

				stream.Close();

				Vector3 position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);
				Quaternion rotation = new Quaternion(data.Rotation[0], data.Rotation[1], data.Rotation[2], data.Rotation[3]);

				Material material = GameManager.Instance.TryGetMaterial(data.MaterialName);

				DraggableObject Obj = GameManager.Instance.TryGetPrefab(data.PrefabName);

				DraggableObject newObj = Instantiate<DraggableObject>(Obj, position, rotation);

				newObj.AllocateName(data.PrefabName);

				if (material != null)
				{
					newObj.ChangeColor(material);
				}

			}
			else
			{
				Debug.LogError("The path is empty - " + path + i);
			}
		}
	}
}
