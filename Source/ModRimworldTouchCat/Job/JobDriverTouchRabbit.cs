// ******************************************************************
//       /\ /|       @file       JobDriverTouchRabbit.cs
//       \ V/        @brief      行为 撸兔子
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 11:28:49
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Linq;
using JetBrains.Annotations;
using Verse;

namespace SR.ModRimWorldTouchCat
{
    [UsedImplicitly]
    public class JobDriverTouchRabbit : JobDriverTouchPet
    {
        protected override void OnToilsSuccess()
        {
            //触发撸兔子的回忆
            pawn.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.SrThoughtTouchRabbit);
            //测试代码
            var hediff1 = HediffMaker.MakeHediff(HediffDefOf.SrHediffAddictionTouchRabbit, pawn);
            hediff1.Severity = 0.6f;
            pawn.health.AddHediff(hediff1);
            CalcNeedTouchRabbit();
        }


        /// <summary>
        /// 计算撸兔子需求
        /// </summary>
        private void CalcNeedTouchRabbit()
        {
            //如果存在撸猫需求 恢复到最高水平
            var allNeeds = pawn.needs.AllNeeds;
            var needTouchRabbit = allNeeds.Where(t => t.def == NeedDefOf.SrNeedTouchRabbit).Cast<NeedTouchPet>()
                .FirstOrDefault();
            //当前角色不存在撸猫需求
            if (needTouchRabbit == null)
            {
                return;
            }

            needTouchRabbit.CurLevel = 1f;
        }
    }
}