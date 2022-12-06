using Sirenix.OdinInspector;
using Tools.Types;
using UnityEngine;

/// <summary>
/// Heart can be damaged by error. Defensive objective for the player.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class HeartScript : Singleton<HeartScript>
{
	[Header("Gameplay")]
	[SerializeField] private int startHealth = 10;

	[Header("SFX")]
	[SerializeField, Required] private AudioClip damageSfx;
	[SerializeField, Required] private AudioClip deathSfx;

	[Header("Visuals")]
	[SerializeField, Required] private Sprite deadSprite;
	[SerializeField, Required] private Canvas canvas;

	private int _health;

	private AudioSource _audio;
	private AudioSource Audio => _audio != null ? _audio : _audio = GetComponent<AudioSource>();

	private SpriteRenderer _sprite;
	private SpriteRenderer Sprite => _sprite != null ? _sprite : _sprite = GetComponent<SpriteRenderer>();

	private void Start()
	{
		_health = startHealth;
		canvas.gameObject.SetActive(false);
	}

	public void Damage(int amount)
	{
		if (_health <= 0) return;

		_health -= amount;
		if (_health > 0) // alive
		{
			Audio.clip = damageSfx;
		}
		else // dead
		{
			Audio.clip = deathSfx;
			Sprite.sprite = deadSprite;
			canvas.gameObject.SetActive(true);
		}
		Audio.Play();
	}
}