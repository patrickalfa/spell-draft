using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////

    [Header("Interpolation")]
    public float interpolationSpeed;

    //----------------------------------------------------------------------//

    private ELEMENT _type;
    private bool _faceUp = false;

    //--HIDDEN-REFERENCES---------------------------------------------------//

    [Header("Sprite Renderers")]
    [SerializeField] private SpriteRenderer _sprtFace;
    [SerializeField] private SpriteRenderer _sprtBack;
    [SerializeField] private TextMeshProUGUI _text;

    //--GETTERS-&-SETTERS---------------------------------------------------//

    public ELEMENT Type
    {
        get { return _type; }
        set
        {
            _type = value;
            _sprtFace.sprite = GameManager.Elements[(int)_type].FaceSprite;
        }
    }

    public bool FaceUp
    {
        get { return _faceUp; }
        set
        {
            _faceUp = value;
            _sprtFace.enabled = _faceUp;
        }
    }

    public int OrderInDeck
    {
        set
        {
            _sprtFace.sortingOrder = -value;
            _sprtBack.sortingOrder = -value;
        }
    }

    public int Points
    {
        set
        {
            _text.text = value > 0 ? value.ToString() : "";
        }
    }

    //////////////////////////////////////////////////////////////////////////////

    private void Update()
    {
        transform.rotation = GetInterpolatedRotation();
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * interpolationSpeed);
    }

    private Quaternion GetInterpolatedRotation()
    {
        Vector3 eulerAngles = transform.rotation.eulerAngles;

        float yRotation = Mathf.Lerp(eulerAngles.y, _faceUp ? 0f : 180f, Time.deltaTime * interpolationSpeed);

        return Quaternion.Euler(eulerAngles.x, yRotation, eulerAngles.z);
    }

    //////////////////////////////////////////////////////////////////////////
}
