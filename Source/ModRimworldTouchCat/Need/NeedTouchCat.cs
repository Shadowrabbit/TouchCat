// ******************************************************************
//       /\ /|       @file       NeedTouchCat.cs
//       \ V/        @brief      撸猫需求 由撸猫成瘾Hediff控制存在
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 18:08:00
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using Verse;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class NeedTouchCat : Need
	{
		private const float ThreshDesire = 0.01f; //渴望临界值
		private const float ThreshSatisfied = 0.1f; //满足临界值
		public override int GUIChangeArrow => -1;
		public override void SetInitialLevel() => CurLevelPercentage = Rand.Range(0.8f, 1f); //初始水平

		public NeedTouchCat(Pawn pawn)
			: base(pawn)
		{
		}

		private Hediff AddictionHediff => pawn.health.hediffSet.hediffs.FirstOrDefault(hediff => hediff.def.causesNeed == def); //成瘾hediff
		/// <summary>
		/// 成瘾程度
		/// </summary>
		public DrugDesireCategory CurCategory
		{
			get
			{
				//成瘾满足状态
				if (CurLevel > ThreshSatisfied)
				{
					return DrugDesireCategory.Satisfied;
				}
				//戒断中或者寻求中
				return CurLevel < ThreshDesire ? DrugDesireCategory.Withdrawal : DrugDesireCategory.Desire;
			}
		}

		/// <summary>
		/// 需求水平
		/// </summary>
		public override float CurLevel
		{
			get => base.CurLevel;
			set
			{
				//设置前的成瘾状态
				var curCategory = CurCategory;
				base.CurLevel = value;
				if (CurCategory == curCategory)
				{
					return;
				}
				//刷新hediff
				pawn.health.Notify_HediffChanged(AddictionHediff);
			}
		}

		/// <summary>
		/// 需求水平间隔一段时间下降
		/// </summary>
		public override void NeedInterval()
		{
			if (IsFrozen)
			{
				return;
			}
			CurLevel -= def.fallPerDay / 400f;
		}
	}
}
