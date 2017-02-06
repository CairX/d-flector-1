using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_DestroyExit : MonoBehaviour {

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);

    }
}
