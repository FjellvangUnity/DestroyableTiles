using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileDestructScript : MonoBehaviour {

    Tilemap map;
    GridLayout gridLayout;
	// Use this for initialization
	void Start () {
        map = GetComponent<Tilemap>();
        gridLayout = transform.parent.GetComponentInParent<GridLayout>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            ContactPoint2D[] contacts;
            contacts = collision.contacts;
            Vector3Int cellPosition = gridLayout.WorldToCell(contacts[0].point);
            map.SetTile(cellPosition, null);
        }
    }
}
