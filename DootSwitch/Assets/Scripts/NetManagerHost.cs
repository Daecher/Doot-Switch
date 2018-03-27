using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetManagerHost : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void OnStartClient()
	{
		this.GetComponent<NetworkManager>().networkAddress = "192.168.0.14";
	}
	
}
