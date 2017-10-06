using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerReferences : MonoBehaviour
{

	public static GameObject _player;
	public GameObject player;
	public static string _playerTag;
	public static string _enemyTag;
	public string playerTag;
	public string enemyTag;

	void OnEnable()
	{
		_playerTag = playerTag;
		_enemyTag = enemyTag;
		_player = player;
	}
}
}