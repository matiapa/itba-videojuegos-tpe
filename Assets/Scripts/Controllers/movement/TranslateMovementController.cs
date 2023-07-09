using UnityEngine;
public class TranslateMovementController : MonoBehaviour, IMovable {
	public void move(Vector3 dir, float speed) {
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
	}
	
}
