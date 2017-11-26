using UnityEngine;
using System.Collections;

public class RandomAudio : MonoBehaviour {

	public AudioClip[] _gravel;
	private AudioSource _aSource;

	void Start()
	{
		// Get attach audio source once.
		_aSource = GetComponent<AudioSource>();

		// Start Coroutine for playing sounds. Call it once
		StartCoroutine("PlaySound");
	}

	void Update()
	{
	}

	IEnumerator PlaySound()
	{
		_aSource.clip = _gravel[Random.Range(0,_gravel.Length)];
		_aSource.Play();
		yield return new WaitForSeconds(_aSource.clip.length);
		StartCoroutine("PlaySound");

	}

}