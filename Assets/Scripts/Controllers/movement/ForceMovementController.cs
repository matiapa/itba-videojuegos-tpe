using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ForceMovementController : MonoBehaviour, IMovable {
	public void move(Vector3 dir, float speed) {
		Quaternion targetRotation = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10f * Time.deltaTime);

		GetComponent<Rigidbody>().AddForce(speed * dir.normalized, ForceMode.VelocityChange);
	}

}
