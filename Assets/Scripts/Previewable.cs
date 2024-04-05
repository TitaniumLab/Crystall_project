using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Previewable : MonoBehaviour, IPreviewable
{
    private Collider _collider;
    private Rigidbody _rb;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void EnablePreviewState(bool isEnabled)
    {
        _collider.enabled = !isEnabled;
        _rb.useGravity = !isEnabled;
    }
}
