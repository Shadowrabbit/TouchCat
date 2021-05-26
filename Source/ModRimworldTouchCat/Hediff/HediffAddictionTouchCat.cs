// ******************************************************************
//       /\ /|       @file       HediffAddictionTouchCat.cs
//       \ V/        @brief		 撸猫buff
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 19:45:58
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class HediffAddictionTouchCat : HediffWithComps
	{
		private const int DefaultStageIndex = 0; //默认阶段
		private const int WithdrawalStageIndex = 1; //戒断阶段
		//当前hediff阶段
		public override int CurStageIndex => Need == null || Need.CurCategory != DrugDesireCategory.Withdrawal
			? DefaultStageIndex
			: WithdrawalStageIndex;
		//撸猫需求
		private NeedTouchCat Need
		{
			get
			{
				if (pawn.Dead)
				{
					return null;
				}
				var allNeeds = pawn.needs.AllNeeds;
				return allNeeds.Where(t => t.def == def.causesNeed).Cast<NeedTouchCat>().FirstOrDefault();
			}
		}
		/// <summary>
		/// 括号中的标签
		/// </summary>
		public override string LabelInBrackets
		{
			get
			{
				var labelInBrackets = base.LabelInBrackets;
				var stringPercent = (1f - Severity).ToStringPercent("F0");
				if (def.CompProps<HediffCompProperties_SeverityPerDay>() == null)
					return labelInBrackets;
				return !labelInBrackets.NullOrEmpty() ? $"{labelInBrackets}{stringPercent}" : stringPercent;
			}
		}
		/// <summary>
		/// 提示
		/// </summary>
		public override string TipStringExtra => Need != null
			? $"{"CreatesNeed".Translate()}:{Need.LabelCap}({Need.CurLevelPercentage.ToStringPercent("F0")})"
			: null;
	}
}
