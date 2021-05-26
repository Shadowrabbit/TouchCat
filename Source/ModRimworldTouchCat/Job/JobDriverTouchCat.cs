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
            //测试代码
            var hediff1 = HediffMaker.MakeHediff(HediffDefOf.SrHediffAddictionTouchCat, pawn);
            hediff1.Severity = 0.6f;
            pawn.health.AddHediff(hediff1);
            CalcNeedTouchCat();
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