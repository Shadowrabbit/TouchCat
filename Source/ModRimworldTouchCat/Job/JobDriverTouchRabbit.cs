// ******************************************************************
//       /\ /|       @file       JobDriverTouchRabbit.cs
//       \ V/        @brief      行为 撸兔子
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 11:28:49
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using System.Collections.Generic;
using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class JobDriverTouchRabbit : JobDriverTouchCat
	{
		private Pawn Rabbit => (Pawn)job.GetTarget(TargetIndex.A); //兔子


		protected override IEnumerable<Toil> MakeNewToils()
		{
			//兔子倒地判定行为失败
			this.FailOnDownedOrDead(TargetIndex.A);
			//走到兔子附近
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
			//走到兔子附近的时候 猫已经死了的情况
			if (Rabbit.Dead)
			{
				yield break;
			}

			//撸1秒
			yield return Toils_General.WaitWith(TargetIndex.A, InteractiveTick, true, true);
			//触发撸兔子的回忆
			pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.SrThoughtTouchRabbit);
		}
	}
}
