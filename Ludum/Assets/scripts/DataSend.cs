using UnityEngine;
using System.Collections;

public class DataSend : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info) {
		if (stream.isWriting) {

		} else {

		}
	}
}
