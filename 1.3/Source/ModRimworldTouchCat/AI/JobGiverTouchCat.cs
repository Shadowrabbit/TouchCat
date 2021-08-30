// ******************************************************************
//       /\ /|       @file       JobGiverTouchCat.cs
//       \ V/        @brief      行为树action节点 撸猫
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-25 16:06:39
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using System;
using JetBrains.Annotations;
using RimWorld;
using Verse;
using Verse.AI;

namespace SR.ModRimWorldTouchCat
{
    [UsedImplicitly]
    public class JobGiverTouchCat : ThinkNode_JobGiver
    {
        private const float MaxDistanceToTouch = 20f; //最大触发距离
        private const int SkillAnimalLevelRequire = 5; //需要的驯兽等级

        /// <summary>
        /// 尝试分配工作
        /// </summary>
        /// <param name="pawn"></param>	
        /// <returns></returns>
        protected override Job TryGiveJob(Pawn pawn)
        {
            //健康状态不好时优先休息 成瘾则忽视健康
            if (HealthAIUtility.ShouldSeekMedicalRest(pawn) && !pawn.IsInPetAddction())
            {
                return null;
            }

            var cat = FindCat(pawn);
            //如果能找到猫就尝试分配撸猫行为
            return cat == null ? null : JobMaker.MakeJob(JobDefOf.SrJobTouchCat, cat);
        }

        /// <summary>
        /// 在小人当前的地图 30单位范围内找只猫
        /// </summary>
        /// <returns></returns>
        private static Pawn FindCat(Pawn pawn)
        {
            //尝试在附近寻找猫
            var currentMap = pawn.Map;
            //最大距离内 最近的 满足驯兽等级的 名单内的(非玩家角色没有此限制)
            Pawn targetAnimal = null;
            var cacheDistance = -1f;
            foreach (var anyPawn in currentMap.mapPawns.AllPawnsSpawned)
            {
                //当前动物已被预订 无法保留给当前角色
                if (!pawn.CanReserve(anyPawn))
                {
                    continue;
                }

                var distance = anyPawn.Position.DistanceTo(pawn.Position);
                //距离超过上限
                if (distance > (pawn.IsInPetAddction() ? MaxDistanceToTouch * 10 : MaxDistanceToTouch))
                {
                    continue;
                }

                //当前pawn种族不是猫
                if (!anyPawn.kindDef.race.defName.Equals(ModDef.RaceDefNameCat))
                {
                    continue;
                }

                //驯兽等级不满足
                if (pawn.skills.GetSkill(SkillDefOf.Animals).Level < SkillAnimalLevelRequire)
                {
                    continue;
                }

                //动物死亡或倒地
                if (anyPawn.Dead || anyPawn.Downed)
                {
                    continue;
                }

                //距离没有之前缓存过的近
                if (distance >= cacheDistance && Math.Abs(cacheDistance + 1) >= 0.001f)
                {
                    continue;
                }

                //当前动物与玩家敌对
                if (anyPawn.Faction != null && anyPawn.Faction.HostileTo(Faction.OfPlayer))
                {
                    continue;
                }

                cacheDistance = distance;
                targetAnimal = anyPawn;
            }

            return targetAnimal;
        }
    }
}