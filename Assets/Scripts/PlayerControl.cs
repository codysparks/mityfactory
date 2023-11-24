using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D player;
    public Animator playerAnimation;

    public float playerMoveSpeed;
    public bool playerLocked;

    private float playerlastMoveX;
    private float playerlastMoveY;

    public ItemControl ItemInteraction;
    public List<ItemControl> items;

    // Start is called before the first frame update
    void Start()
    {
        items = new List<ItemControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // Allow player to move
        if (!playerLocked) {
            player.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerMoveSpeed;
        }
        else {
            player.velocity = Vector2.zero;
        }

        playerAnimation.SetFloat("moveX", player.velocity.x);
        playerAnimation.SetFloat("moveY", player.velocity.y);

        // Control the direction of the player
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1) {
            if (!playerLocked) {
                playerAnimation.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                playerAnimation.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));

                playerlastMoveX = Input.GetAxisRaw("Horizontal");
                playerlastMoveY = Input.GetAxisRaw("Vertical");
            }
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            // Pick up item
            if (ItemInteraction) {
                if (CountItems() == 0) {
                    AddItem(ItemInteraction);
                    ItemInteraction.HideItem();
                }
                else {
                    Debug.Log("Inventory Full");
                }
            } else if (CountItems() > 0) {
                RemoveItem();
            }
        }
    }

    public void SetInteraction(ItemControl item) {
        ItemInteraction = item;
    }

    public void AddItem(ItemControl item) {
        items.Add(item);
    }

    public void RemoveItem() {
        // Facing Up
        if(playerlastMoveY > 0) {
            items[0].ShowItem(new Vector3(player.transform.position.x + playerlastMoveX, player.transform.position.y - .5f, player.transform.position.z));
        }
        // Facing Down
        else if(playerlastMoveY < 0) {
            items[0].ShowItem(new Vector3(player.transform.position.x + playerlastMoveX, player.transform.position.y - 2.1f, player.transform.position.z));
        }
        // Facing Left or Right
        else {
            items[0].ShowItem(new Vector3(player.transform.position.x + playerlastMoveX, player.transform.position.y - 1.5f, player.transform.position.z));
        }

        items = new List<ItemControl>();
    }

    public int CountItems() {
        return items.Count;
    }
}
