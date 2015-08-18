using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	public GameObject poi;

	void Update () {
		Vector3 pos = poi.transform.position;
		pos.z = transform.position.z;
		transform.position = pos;
	}
}
