﻿using UnityEngine;
using System.Collections;

//just attach this script to player

//how to tweak:
//rb.drag below slows player down
//Edit > Project > Input > Horizontal | Vertical > Gravity = ++## slows down trail off of input 
//recommend at least 3:1 ratio of moveForce to drag for snappy controls.  3 * moveForce = 1 * drag.
public class Player : Physical
{

    public float moveForce = 40f;

    Rigidbody2D rb;
    Collider2D pc;

    [HideInInspector]
    public Tile storedTile;
    public bool isInShuffler = false;
    CamZoom camZoomer;
    Map map;

    void Awake()
    {
        InitPlayer();

    }

    void Start() {
        map = GameObject.Find("Map").GetComponent<Map>();
        camZoomer = Camera.main.GetComponent<CamZoom>();
        camZoomer.ZoomTo(map.alterTile.transform);
    }

    void Update()
    {

    }

    void InitPlayer()
    {
        gameObject.tag = "Player";
        gameObject.name = "Player";
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        pc = gameObject.AddComponent<CircleCollider2D>();
        rb.freezeRotation = true;
        rb.drag = 15f;

        storedTile = transform.parent.GetComponent<Tile>();
        //Debug.Log(storedTile.name);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //shuffler stuff
        Shuffler shufflerTriggerable = other.GetComponent<Shuffler>();
        if (shufflerTriggerable != null)
        {
            isInShuffler = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Tile enteringTile = other.GetComponent<Tile>();
        if (enteringTile != null) {
            storedTile = enteringTile;
            transform.parent = storedTile.transform;
            camZoomer.ZoomTo(storedTile.transform);
            enteringTile.RevealTile();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        Shuffler shufflerTriggerable = other.GetComponent<Shuffler>();
        if (shufflerTriggerable != null)
        {
            isInShuffler = true;
        }
    }
}
