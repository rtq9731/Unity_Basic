using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerOfMine : MonoBehaviour
{
    //[SerializeField] GameObject afterImagePrefab;
    //[SerializeField] int effectPoolMax = 0;

    //private GameObject player = null;

    //[SerializeField] private float aiCreateTumb = 1; // afterImageCreateTumb
    //public float lastCreateTerm = 0; // lastCreateTerm

    //private bool isCreatingAfterImage;

    //Queue<GameObject> afterEffects = new Queue<GameObject>();

    //private void Update()
    //{
    //    if (isCreatingAfterImage)
    //    {
    //        if (Time.time <= (lastCreateTerm + aiCreateTumb))
    //        {
    //            Debug.Log(lastCreateTerm + aiCreateTumb);
    //            MakeAfterImageEffect();
    //        }
    //    }
    //}

    //public void MakeAfterImage(GameObject player)
    //{
    //    isCreatingAfterImage = true;
    //    this.player = player;
    //}

    //public void MakeAfterImageEffect()
    //{

    //    SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
    //    if (afterEffects.Count <= effectPoolMax)
    //    {
    //        GameObject obj = Instantiate(afterImagePrefab, player.transform.position, Quaternion.identity);
    //    }

    //    AfterImage img = afterEffects.Dequeue().GetComponent<AfterImage>();
    //    img.SetSprite(playerRenderer.sprite, playerRenderer.flipX, transform.position, afterEffects);
    //    lastCreateTerm = Time.time;
    //    Debug.Log(lastCreateTerm);
    //}

}
