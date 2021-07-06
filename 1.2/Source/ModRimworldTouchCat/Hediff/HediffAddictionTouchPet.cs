// ******************************************************************
//       /\ /|       @file       HediffAddictionTouchPet.cs
//       \ V/        @brief		 撸宠物成瘾buff
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
    public class HediffAddictionTouchPet : HediffWithComps
    {
        private const int DefaultStageIndex = 0; //默认阶段

        private const int WithdrawalStageIndex = 1; //戒断阶段

        //当前hediff阶段
        public override int CurStageIndex => Need == null || Need.CurCategory != DrugDesireCategory.Withdrawal
            ? DefaultStageIndex
            : WithdrawalStageIndex;

        //撸猫需求
        private NeedTouchPet Need
        {
            get
            {
                if (pawn.Dead)
                {
                    return null;
                }

                var allNeeds = pawn.needs.AllNeeds;
                return allNeeds.Where(t => t.def == def.causesNeed).Cast<NeedTouchPet>().FirstOrDefault();
            }
        }

        /// <summary>
        /// 提示
        /// </summary>
        public override string TipStringExtra => Need != null
            ? $"{"CreatesNeed".Translate()}:{Need.LabelCap}({Need.CurLevelPercentage.ToStringPercent("F0")})"
            : null;

        /// <summary>
        /// 需求类别改变
        /// </summary>
        public void Notify_NeedCategoryChanged() => pawn.health.Notify_HediffChanged(this);
    }
}