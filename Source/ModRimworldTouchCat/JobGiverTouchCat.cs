// ******************************************************************
//       /\ /|       @file       JobGiverTouchCat.cs
//       \ V/        @brief      行为树action节点 撸猫
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-25 16:06:39
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class JobGiverTouchCat : ThinkNode_JobGiver
	{
		protected const float MaxDistanceToTouch = 30f;

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

			var cat = FindCat(pawn);
			//如果能找到猫就尝试分配撸猫行为
			return cat == null ? null : JobMaker.MakeJob(JobDefOf.SrJobTouchCat, cat);
		}

		/// <summary>
		/// 在小人当前的地图 20单位范围内找只猫
		/// </summary>
		/// <returns></returns>
		private static Pawn FindCat(Thing pawn)
		{
			//尝试在附近寻找猫
			var currentMap = pawn.Map;
			foreach (var anyPawn in currentMap.mapPawns.AllPawnsSpawned)
			{
				//迭代器中当前的pawn离我们的小人距离超过30个单位 太远了 不触发
				if (anyPawn.Position.DistanceTo(pawn.Position) > MaxDistanceToTouch)
				{
					continue;
				}

				//当前pawn种族不是猫
				if (!anyPawn.kindDef.defName.Equals("Cat"))
				{
					continue;
				}

				return anyPawn;
			}

			return null;
		}
	}
}
