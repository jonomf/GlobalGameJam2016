using UnityEngine;

public class Layers {

	public const string Player = "Player";
	public const string PlayerArrow = "PlayerArrow";
	public const string Collectable = "Collectable";

	public static int PlayerNum { get; private set; }
	public static int PlayerArrowNum { get; private set; }
	public static int CollectableNum { get; private set; }

	public static void Init() {
		PlayerNum = LayerMask.NameToLayer(Player);
		PlayerArrowNum = LayerMask.NameToLayer(PlayerArrow);
		CollectableNum = LayerMask.NameToLayer(Collectable);
	}
}
