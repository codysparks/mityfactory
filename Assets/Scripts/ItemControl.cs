using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour
{

    public PlayerControl Player;
    public SpriteRenderer parentSpriteRenderer;
    public BoxCollider2D parentCollider;

    // Start is called before the first frame update
    void Start()
    {
        parentSpriteRenderer = GetComponentInParent<SpriteRenderer>();
        parentCollider = GetComponentInParent<BoxCollider2D>();
        Player = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.SetInteraction(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.SetInteraction(gameObject.AddComponent<ItemControl>());
        }
    }

    public string GetName()
    {
        return this.name;
    }

    public void ShowItem(Vector3 position)
    {
        parentSpriteRenderer.transform.position = position;
        parentSpriteRenderer.enabled = true;
        parentCollider.enabled = true;
    }

    public void HideItem()
    {
        parentSpriteRenderer.enabled = false;
        parentCollider.enabled = false;
    }
}
