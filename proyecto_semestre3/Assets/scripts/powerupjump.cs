using UnityEngine;
using System.Collections;

public class PowerUpJump : MonoBehaviour
{
    [SerializeField] private float _jumpStrength = 10;
    [SerializeField] private float _resetTime = 2;
    [SerializeField] private GameObject _powerUpVisuals = null;
    [SerializeField] private Collider _collider = null;

    private JumpController _jumpController = null;
    private float _previousJumpStrength = default;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _collider.enabled = false;
        _powerUpVisuals.SetActive(false);

        _jumpController = other.gameObject.GetComponent<JumpController>();
        _previousJumpStrength = _jumpController.JumpStrength;
        _jumpController.ChangeJumpStrength(_jumpStrength);

        StartCoroutine(ResetPowerUp());
    }

    private IEnumerator ResetPowerUp()
    {
        yield return new WaitForSeconds(_resetTime);
        _jumpController.ChangeJumpStrength(_previousJumpStrength);

        Destroy(gameObject);
    }
}
