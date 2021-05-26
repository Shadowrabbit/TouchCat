// ******************************************************************
//       /\ /|       @file       NeedDefOf.cs
//       \ V/        @brief		 需求定义
//       | "")       @author     Shadowrabbit, yingtu0401@gmail.com
//       /  |                    
//      /  \\        @Modified   2021-05-26 18:47:51
//    *(__\_\        @Copyright  Copyright (c) 2021, Shadowrabbit
// ******************************************************************

using JetBrains.Annotations;
using RimWorld;

namespace SR.ModRimWorldTouchCat
{
	[DefOf]
	public static class NeedDefOf
	{
		[UsedImplicitly] public static readonly NeedDef SrNeedTouchCat; //定期撸猫的需求
		[UsedImplicitly] public static readonly NeedDef SrNeedTouchRabbit; //定期撸兔子的需求
	}
}
