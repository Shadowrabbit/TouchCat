// ******************************************************************
//       /\ /|       @file       JobDriverTouchCat.cs
//       \ V/        @brief      行为 撸猫
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-25 17:23:36
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class JobDriverTouchCat : JobDriver
	{
		private Pawn Cat => (Pawn)job.GetTarget(TargetIndex.A); //猫
		protected const int InteractiveTick = 60; //交互时长

		/// <summary>	
		/// 尝试行为前预定
		/// </summary>
		/// <param name="errorOnFailed"></param>
		/// <returns></returns>
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return true;
		}

		/// <summary>
		/// A是猫
		/// </summary>
		/// <returns></returns>
		protected override IEnumerable<Toil> MakeNewToils()
		{
			//猫倒地判定行为失败
			this.FailOnDownedOrDead(TargetIndex.A);
			//走到猫附近
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
			//走到猫附近的时候 猫已经死了的情况
			if (Cat.Dead)
			{
				yield break;
			}

			//撸1秒
			yield return Toils_General.WaitWith(TargetIndex.A, InteractiveTick, true, true);
			//触发撸猫的回忆
			pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.SrThoughtTouchCat);
		}
	}
}
