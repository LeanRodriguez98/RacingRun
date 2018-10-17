using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLifeBar : MonoBehaviour {
    public Texture fullLife;
    public Texture twoThirdsLife;
    public Texture oneThirdLife;
    public Texture emptyLife;
    private Player playerInstance;
    private RawImage rawImage;
    private int auxPlayerLife;
    // Use this for initialization
    void Start () {
        playerInstance = Player.instance;
        rawImage = GetComponent<RawImage>();
        auxPlayerLife = playerInstance.life;
    }
	
	// Update is called once per frame
	void Update () {
        if (auxPlayerLife != playerInstance.life)
        {
            if (playerInstance.life > 70)
            {
                rawImage.texture = fullLife;
            }
            else if (playerInstance.life > 40 && playerInstance.life <= 70)
            {
                rawImage.texture = twoThirdsLife;
            }
            else if (playerInstance.life > 10 && playerInstance.life <= 40)
            {
                rawImage.texture = oneThirdLife;
            }
            else if (playerInstance.life < 10)
            {
                rawImage.texture = emptyLife;
            }
            auxPlayerLife = playerInstance.life;

        }
    }
}
