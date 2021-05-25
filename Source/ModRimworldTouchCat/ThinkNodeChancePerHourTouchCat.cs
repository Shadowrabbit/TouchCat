// ******************************************************************
//       /\ /|       @file       ThinkNodeChancePerHourTouchCat.cs
//       \ V/        @brief      行为树条件节点 每小时触发撸猫的概率
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-25 16:39:05
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using JetBrains.Annotations;
using Verse;
using Verse.AI;

namespace SR.ModRimworldTouchCat
{
	[UsedImplicitly]
	public class ThinkNodeChancePerHourTouchCat : ThinkNode_ChancePerHour
	{
		/// <summary>
		/// 12分之一的概率触发撸猫 一天两次不过分吧
		/// </summary>
		/// <param name="pawn"></param>
		/// <returns></returns>
		protected override float MtbHours(Pawn pawn)
		{
			return 12f;
		}
	}
}
