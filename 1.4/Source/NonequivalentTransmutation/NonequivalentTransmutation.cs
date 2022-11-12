using System;
using UnityEngine;
using Verse;
using RimWorld;

namespace NonequivalentTransmutation
{
	[StaticConstructorOnStartup]
	public class CompPowerPlantCollector : CompPowerPlant
	{
		protected override float DesiredPowerOutput
		{
			get
			{
				return Mathf.Lerp(0f, 15000f, this.parent.Map.skyManager.CurSkyGlow) * this.RoofedPowerOutputFactor;
			}
		}
		private float RoofedPowerOutputFactor
		{
			get
			{
				int num = 0;
				int num2 = 0;
				foreach (IntVec3 c in this.parent.OccupiedRect())
				{
					num++;
					if (this.parent.Map.roofGrid.Roofed(c))
					{
						num2++;
					}
				}
				return (float)(num - num2) / (float)num;
			}
		}
		public override void PostDraw()
		{
			base.PostDraw();
			GenDraw.FillableBarRequest r = default(GenDraw.FillableBarRequest);
			r.center = this.parent.DrawPos + Vector3.up * 0.1f;
			r.size = CompPowerPlantCollector.BarSize;
			r.fillPercent = base.PowerOutput / 15000f;
			r.filledMat = CompPowerPlantCollector.PowerPlantCollectorBarFilledMat;
			r.unfilledMat = CompPowerPlantCollector.PowerPlantCollectorBarUnfilledMat;
			r.margin = 0.15f;
			Rot4 rotation = this.parent.Rotation;
			rotation.Rotate(RotationDirection.Clockwise);
			r.rotation = rotation;
			GenDraw.DrawFillableBar(r);
		}

		private const float FullSunPower = 15000f;

		private const float NightPower = 0f;

		private static readonly Vector2 BarSize = new Vector2(2.3f, 0.14f);

		private static readonly Material PowerPlantCollectorBarFilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.5f, 0.475f, 0.1f), false);

		private static readonly Material PowerPlantCollectorBarUnfilledMat = SolidColorMaterials.SimpleSolidColorMaterial(new Color(0.15f, 0.15f, 0.15f), false);
	}
}
