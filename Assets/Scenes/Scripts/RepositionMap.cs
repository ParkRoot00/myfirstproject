using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RepositionMap : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area"))
            return;

        Vector3 playerPos = GameManager.instance.player.transform.position; //player position save
        Vector3 myPos = transform.position;
        float dirX = playerPos.x - myPos.x;
        float dirY = playerPos.y - myPos.y;

        float diffX = Mathf.Abs(dirX);
        float diffY = Mathf.Abs(dirY);

        Vector3 playerDir = GameManager.instance.player.inputVec;
        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;
        
        switch (transform.tag)
        {
            case "Ground":
                if (diffX > diffY)
                {
                    transform.Translate(Vector3.right * dirX * 52);
                }
                else if(diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 36);
                }
                break;
            case "Enemy":

                break;
        }
        

    }
}
