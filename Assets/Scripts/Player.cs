﻿using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[Header("Settings")]
	public float speed;
	public float arrowForce = 1000;
	public float arrowReticleDistance = 2f;
	[Header("Audio")]
	public AudioSource collectAudio;
	[Header("References")]
	public GameObject arrowPrefab;
	public GameObject arrowReticlePrefab;

	private Transform arrowReticle;
	private Vector3 movement;

	private Vector3 Offscreen { get { return Vector3.down * 10000; } }

	Animator anim;


	void Start() {
		arrowReticle = (Instantiate(arrowReticlePrefab, Offscreen, Quaternion.identity) as GameObject).transform;
		anim = GetComponent<Animator> ();
	}

	void Update () {
		Move();
		Attack();
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.layer == Layers.CollectableNum) {
			Collect(collision.gameObject);
		}
	}

	private void Move() {
		movement = Vector3.zero;
		if (Input.GetKey(KeyCode.W)) {
			movement += Vector3.up;
			anim.Play("runUp");
		}
		else if (Input.GetKey(KeyCode.S)) {
			movement += Vector3.down;
			anim.Play ("runDown");
		}
		if (Input.GetKey(KeyCode.D)) {
			movement += Vector3.right;
			anim.Play ("runRight");
		}
		else if (Input.GetKey(KeyCode.A)) {
			movement += Vector3.left;
			anim.Play ("runLeft");
		}
		GetComponent<Rigidbody2D>().MovePosition(transform.position + movement * speed * Time.deltaTime);

		if (Input.anyKey == false) {
			anim.Play("idleDown");
		}
	}
		

	private Vector3 shootAngle;
	private void Attack() {
		shootAngle = Vector3.zero;
		if (Input.GetKey(KeyCode.UpArrow)){
			shootAngle += Vector3.up;
		} else if (Input.GetKey(KeyCode.DownArrow)){
			shootAngle += Vector3.down;
		}
		if (Input.GetKey(KeyCode.LeftArrow)){
			shootAngle += Vector3.left;
		} else if (Input.GetKey(KeyCode.RightArrow)){
			shootAngle += Vector3.right;
		}
		arrowReticle.position = transform.position + shootAngle * arrowReticleDistance;

		if (Input.GetKeyDown(KeyCode.Space)){
			ShootArrow(shootAngle);
		}
	}

	private void ShootArrow(Vector3 direction) {
		Rigidbody2D arrowRb = (Instantiate(arrowPrefab, transform.position, Quaternion.Euler(direction)) as GameObject).GetComponent<Rigidbody2D>();
		arrowRb.AddForce(direction * arrowForce);
		arrowReticle.position = Offscreen;
	}

	private void Collect(GameObject obj) {
		Destroy(obj);
		collectAudio.Play();
	}
}
