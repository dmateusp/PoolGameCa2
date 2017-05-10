using UnityEngine;

public class ReceiveBall : MonoBehaviour {

    private GameObject gameController;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    void OnTriggerEnter(Collider other)
    {
        gameController.SendMessage("BallPotted",  other);
        if(other.tag != "WhiteBall" && other.tag != "Cue")
            Destroy(other.gameObject);
    }
}
 