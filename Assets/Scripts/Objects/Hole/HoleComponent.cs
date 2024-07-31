using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleComponent : MonoBehaviour
{
	public HoleEntity hole {get; private set;}

	public virtual void Init(HoleEntity hole) => this.hole = hole;
}
