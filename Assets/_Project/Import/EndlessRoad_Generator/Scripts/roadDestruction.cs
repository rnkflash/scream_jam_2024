//written by Boris Chuprin smokerr@mail.ru
//this script destroys road segments when it's far away from player
//

using System;
using UnityEngine;
using System.Collections;


public class roadDestruction : MonoBehaviour {

	public int destroyInMeters = 300;//300 by default is distance of 300 meters when road segment should be destroyed, because it's too far and you don't want to keep it in memory
	//if you want to keep read longer then make it more as you need

	private GameObject playerDriver;//define player vehicle

}
