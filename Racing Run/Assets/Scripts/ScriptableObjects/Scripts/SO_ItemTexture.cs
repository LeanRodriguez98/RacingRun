using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "TextureItemData", menuName = "SO/ItemTexture")]
public class SO_ItemTexture : ScriptableObject {

    public Texture icon;
    public int price;
    public Material material;
    public bool boughted;
}
