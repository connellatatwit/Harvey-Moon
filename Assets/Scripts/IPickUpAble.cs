using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpAble
{
    public ScoreEntity ScoreEntity { get; }
    public void SetFollow();
}
