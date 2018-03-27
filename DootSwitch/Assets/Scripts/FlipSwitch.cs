using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FlipSwitch : NetworkBehaviour {
	
	AudioSource doot;
	[SerializeField] Material on;
	[SerializeField] Material off;
	[SerializeField] bool onOff = false;
	public GameObject YouText;
	bool canFlip;
	

	// Use this for initialization
	void Start () {
		doot = GetComponent<AudioSource>();
		// if (isServer)
		// {
			// GetComponent<Renderer>().material = on;
			// onOff = true;
			// transform.Translate(Vector3.up * 3);
		// }
		if (isServer) onOff = true;
		
		if (isServer)
		{
			if (isLocalPlayer) transform.Translate(Vector3.up );
			else transform.Translate(-Vector3.up);
		}
		// else if (isClient)
		// {
			// if (isLocalPlayer) transform.Translate(-Vector3.up);
			// else transform.Translate(Vector3.up);
		// }
		
		if (!isLocalPlayer)
		{
			transform.tag = "Player";
			onOff = !onOff;
			Debug.Log(transform.tag);
		}
		if (isLocalPlayer) Instantiate(YouText, transform.position, Quaternion.identity);
		
		if (onOff == true) GetComponent<Renderer>().material = on;
		else if (onOff == false) GetComponent<Renderer>().material = off;
		// else GetComponent<Renderer>().material = off;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(Network.connections.Length);
		if (isLocalPlayer)
		{
			if (GameObject.FindGameObjectsWithTag("Player").Length > 0 && transform.parent == null)
			{
				transform.SetParent(GameObject.FindGameObjectsWithTag("Player")[0].transform);
			}
		}
		if (Input.GetKeyDown("space") && onOff == false && isLocalPlayer)
		{
			if (isServer) 
			{
				RpcFlip();
				transform.parent.GetComponent<FlipSwitch>().Flip();
			}
			//Flip();
			else 
			{
				CmdFlip();
				Flip();
				transform.parent.GetComponent<FlipSwitch>().Flip();
			}
		}
		if (onOff == true) GetComponent<Renderer>().material = on;
		else if (onOff == false) GetComponent<Renderer>().material = off;
		
		//else if (Input.GetKeyDown("space")) FlipOff();
	}
	
	public void FlipOff()
	{
		onOff = false;
	}
	
	public void FlipOn()
	{
		onOff = true;
	}
	
	[Command]
	void CmdFlip()
	{
		//doot.Play();
		Debug.Log("I got flipped!");
		if (onOff == true)
		{
			FlipOff();
		}
		else 
		{
			FlipOn();
		}
		if (!isLocalPlayer) transform.GetChild(0).GetComponent<FlipSwitch>().Flip();
	}
	
	[ClientRpc]
	void RpcFlip()
	{
		//doot.Play();
		Debug.Log("I got flipped!");
		if (onOff == true)
		{
			FlipOff();
		}
		else 
		{
			FlipOn();
		}
		if (!isLocalPlayer) transform.GetChild(0).GetComponent<FlipSwitch>().Flip();
	}
	
	public void Flip()
	{
		if (!isLocalPlayer) doot.Play();
		Debug.Log("I got flipped!");
		if (onOff == true)
		{
			FlipOff();
		}
		else 
		{
			FlipOn();
		}
	}
	
}