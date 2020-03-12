using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleSpriteColorChanger : ColorSprite
{
    private SpriteRenderer[] _spriteRenderers;

    // Start is called before the first frame update
    protected override void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        base.Start();
    }

    // Update is called once per frame
    protected override void  Update()
    {

        if (_spriteRenderers.Length != 0 && _controller != null && !_hasOwnColour)
        {
            for(int i = 0; i < _spriteRenderers.Length; i++)
                _spriteRenderers[i].color = _controller._currentColour;
        }
        else
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {                
                _spriteRenderers[i].color = _ownColour;
                if (Time.time > _changeBackTime) _hasOwnColour = false;
            }
        }
    }
}
