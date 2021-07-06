// ******************************************************************
//       /\ /|       @file       PawnExtention.cs
//       \ V/        @brief      角色扩展 
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-06-07 13:09:00
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************
using System.Linq;
using Verse;

namespace SR.ModRimWorldTouchCat
{
	public static class PawnExtention
	{
		/// <summary>
		/// 处于宠物成瘾状态
		/// </summary>
		/// <param name="pawn"></param>
		/// <returns></returns>
		public static bool IsInPetAddction(this Pawn pawn)
		{
			var hediffs = pawn.health.hediffSet.hediffs;
			return hediffs.OfType<HediffAddictionTouchPet>().Any();
		}
	}
}
