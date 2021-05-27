// ******************************************************************
//       /\ /|       @file       JobDriverTouchCat.cs
//       \ V/        @brief      行为 撸猫
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-25 17:23:36
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using UnityEngine;
using Verse;

namespace SR.ModRimWorldTouchCat
{
	[UsedImplicitly]
	public class JobDriverTouchCat : JobDriverTouchPet
	{
		/// <summary>
		/// 全部步骤成功时回调
		/// </summary>
		protected override void OnToilsSuccess()
		{
			//触发撸猫的回忆
			pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.SrThoughtTouchCat);
			//概率触发成瘾	
			CalcAddiction();
			//概率加入殖民者阵营
			CalcJoin();
			//计算需求
			CalcNeedTouchCat();
		}

		/// <summary>
		/// 计算成瘾触发
		/// </summary>
		private void CalcAddiction()
		{
			//已经成瘾 不会重复触发
			if (Enumerable.Any(pawn.health.hediffSet.hediffs, heddif => heddif.def == HediffDefOf.SrHediffAddictionTouchCat))
			{
				return;
			}
			var randomNum = Random.Range(0f, 1f);
			if ((randomNum > ChanceToAddiction))
			{
				return;
			}
			var hediffAddictionTouchCat = HediffMaker.MakeHediff(HediffDefOf.SrHediffAddictionTouchCat, pawn);
			pawn.health.AddHediff(hediffAddictionTouchCat);
			Messages.Message("MsgAddictionTouchCat".Translate(pawn.Label), MessageTypeDefOf.NeutralEvent);
		}

		/// <summary>
		/// 计算加入殖民者
		/// </summary>
		private void CalcJoin()
		{
			//已经是殖民者阵营
			if (Pet.Faction == Faction.OfPlayer)
			{
				return;
			}
			var randomNum = Random.Range(0f, 1f);
			if ((randomNum > ChanceToJoin))
			{
				return;
			}
			Pet.SetFaction(Faction.OfPlayer);
			Messages.Message("MsgTouchPetJoin".Translate(pawn.Label, Pet.Label), MessageTypeDefOf.NeutralEvent);
		}

		/// <summary>
		/// 计算撸猫需求
		/// </summary>
		private void CalcNeedTouchCat()
		{
			//如果存在撸猫需求 恢复到最高水平
			var allNeeds = pawn.needs.AllNeeds;
			var needTouchCat = allNeeds.Where(t => t.def == NeedDefOf.SrNeedTouchCat).Cast<NeedTouchPet>()
				.FirstOrDefault();
			//当前角色不存在撸猫需求
			if (needTouchCat == null)
			{
				return;
			}
			needTouchCat.CurLevel = 1f;
		}
	}
}
