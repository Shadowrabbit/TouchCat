// ******************************************************************
//       /\ /|       @file       ThinkNodeConditionalSatisfyTouchCat.cs
//       \ V/        @brief      行为树条件节点 尝试满足撸猫需求
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 23:53:13
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Linq;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
    [UsedImplicitly]
    public class ThinkNodeConditionalSatisfyTouchCat : ThinkNode_Conditional
    {
        //应该满足需求
        private static bool ShouldSatisfy(Need need) => need is NeedTouchPet needTouchPet &&
                                                        needTouchPet.CurCategory <= DrugDesireCategory.Desire;

        /// <summary>
        /// 是否满足条件
        /// </summary>
        /// <param name="pawn"></param>
        /// <returns></returns>
        protected override bool Satisfied(Pawn pawn)
        {
            var allNeeds = pawn.needs.AllNeeds;
            return Enumerable.Any(allNeeds,
                need => need.def.defName.Equals(MainDef.CatKindDefName) && ShouldSatisfy(need));
        }
    }
}