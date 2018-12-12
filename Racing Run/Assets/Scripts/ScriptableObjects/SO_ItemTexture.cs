using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "TextureItemData", menuName = "SO/ItemTexture")]
public class SO_ItemTexture : ScriptableObject {

    public string iconName;
    public int price;
    public string materialName;
    public bool boughted;
}
