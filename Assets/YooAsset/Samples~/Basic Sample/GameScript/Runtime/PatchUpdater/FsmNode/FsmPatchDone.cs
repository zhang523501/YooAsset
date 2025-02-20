﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class FsmPatchDone : IFsmNode
{
	public string Name { private set; get; } = nameof(FsmPatchDone);

	void IFsmNode.OnEnter()
	{
		PatchEventDispatcher.SendPatchStepsChangeMsg(EPatchStates.PatchDone);
		Debug.Log("补丁流程更新完毕！");

		FsmManager.Transition(nameof(FsmClearCache));
	}
	void IFsmNode.OnUpdate()
	{
	}
	void IFsmNode.OnExit()
	{
	}
}