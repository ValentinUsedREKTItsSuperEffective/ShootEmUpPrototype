using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class GameOver : MonoBehaviour {

    [SerializeField] EntityModel playerModel;
    ReactiveProperty<bool> playerDead;

    [SerializeField] EntityModel homeModel;
    ReactiveProperty<bool> homeDestroyed;

    [SerializeField] Text gameOverText;

	void Start () {
        playerDead = playerModel.currentLife.Select (life => life <= 0).ToReactiveProperty ();
        homeDestroyed = homeModel.currentLife.Select (life => life <= 0).ToReactiveProperty ();

        playerDead.Where(isDead => isDead == true).Subscribe (_ => {
            gameOverText.gameObject.SetActive (true);
        });

        homeDestroyed.Where (isDestroyed => isDestroyed == true).Subscribe (_ => {
            gameOverText.gameObject.SetActive (true);
        });
	}
}
