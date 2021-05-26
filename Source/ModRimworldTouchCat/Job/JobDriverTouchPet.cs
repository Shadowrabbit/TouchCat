// ******************************************************************
//       /\ /|       @file       JobDriverTouchPet.cs
//       \ V/        @brief      行为 撸宠物
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 23:20:29
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
    public abstract class JobDriverTouchPet : JobDriver
    {
        private const int InteractiveTick = 60; //交互时长
        private readonly Toil _toilCacl; //结算步骤
        private Pawn Pet => (Pawn) job.GetTarget(TargetIndex.A); //宠物

        protected JobDriverTouchPet()
        {
            _toilCacl = new Toil()
            {
                initAction = OnToilsSuccess
            };
        }

        /// <summary>	
        /// 尝试行为前预定
        /// </summary>
        /// <param name="errorOnFailed"></param>
        /// <returns></returns>
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        /// <summary>
        /// A是宠物
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Toil> MakeNewToils()
        {
            //宠物倒地判定行为失败
            this.FailOnDownedOrDead(TargetIndex.A);
            //走到宠物附近
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            //走到宠物附近的时候 宠物已经死了的情况
            if (Pet.Dead)
            {
                yield break;
            }

            //撸1秒
            yield return Toils_General.WaitWith(TargetIndex.A, InteractiveTick, true, true);
            yield return _toilCacl;
        }

        /// <summary>
        /// 全部步骤成功时回调
        /// </summary>
        protected abstract void OnToilsSuccess();
    }
}