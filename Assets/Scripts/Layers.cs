using UnityEngine;

public class Layers {

	public const string Collectable = "Collectable";

	public static int CollectableNum { get; private set; }

	public static void Init() {
		CollectableNum = LayerMask.NameToLayer(Collectable);
	}
}
