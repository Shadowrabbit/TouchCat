// ******************************************************************
//       /\ /|       @file       JobGiverTouchRabbit.cs
//       \ V/        @brief      行为树action节点 撸兔子
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 11:25:10
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class JobGiverTouchRabbit : ThinkNode_JobGiver
	{
		private const float MaxDistanceToTouch = 30f; //最大触发距离

		/// <summary>
		/// 尝试分配工作
		/// </summary>
		/// <param name="pawn"></param>
		/// <returns></returns>
		protected override Job TryGiveJob(Pawn pawn)
		{
			//健康状态不好时优先休息
			if (HealthAIUtility.ShouldSeekMedicalRest(pawn))
			{
				return null;
			}

			var rabbit = FindRabbit(pawn);
			//如果能找到兔子就尝试分配撸兔子行为
			return rabbit == null ? null : JobMaker.MakeJob(JobDefOf.SrJobTouchRabbit, rabbit);
		}

		/// <summary>
		/// 在小人当前的地图 30单位范围内找只兔子
		/// </summary>
		/// <returns></returns>
		private static Pawn FindRabbit(Thing pawn)
		{
			//尝试在附近寻找兔子
			var currentMap = pawn.Map;
			foreach (var anyPawn in currentMap.mapPawns.AllPawnsSpawned)
			{
				//迭代器中当前的pawn离我们的小人距离超过30个单位 太远了 不触发
				if (anyPawn.Position.DistanceTo(pawn.Position) > MaxDistanceToTouch)
				{
					continue;
				}

				//当前pawn种族不是兔子
				if (!anyPawn.kindDef.defName.Equals(ModDef.KindDefNameRabbit))
				{
					continue;
				}

				return anyPawn;
			}

			return null;
		}
	}
}
