using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPSSystem
{
public class GameManagerMaster : MonoBehaviour
{

	public delegate void GameManagerEventHandler();
	public event GameManagerEventHandler MenuToggleEvent;
	public event GameManagerEventHandler InventoryUIToggleEvent;
	public event GameManagerEventHandler RestartLevelEvent;
	public event GameManagerEventHandler GoToMenuSceneEvent;
	public event GameManagerEventHandler GameOverEvent;

	public bool isMenuOn;
	public bool isInventoryUIOn;
    public bool isGameOver;

	public void CallEventMenuToggle()
	{
		if (MenuToggleEvent != null)
		{
			MenuToggleEvent();
		}
	}

	public void CallEventInventoryUIToggle()
	{
		if (InventoryUIToggleEvent != null)
		{
			InventoryUIToggleEvent();
		}
	}

	public void CallEventRestartLevel()
	{
		if (RestartLevelEvent != null)
		{
			RestartLevelEvent();
		}
	}

	public void CallEventGoToMenuScene()
	{
		if (GoToMenuSceneEvent != null)
		{
			GoToMenuSceneEvent();
		}
	}

	public void CallEventGameOver()
	{
		if (GameOverEvent != null)
		{
			if (!isGameOver)
			{
				isGameOver = true;
				GameOverEvent();
			}

		}
	}
}
}