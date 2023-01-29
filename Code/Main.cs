using NCMS;
using Cultivation_Way;

namespace Addon_Namespace
{
	[ModEntry]
	public class Addon_Main_Class : CW_Addon
	{
		public override void awake(){
			// 不要在此处添加代码，除非你知道你在做什么
			// DO NOT code here.
			load_mod_info(System.Type.GetType("Mod"));
		}
		public override void initialize(){
			print(string.Format("Hello CW Addon, {0}!", this_mod.Info.Name));
			// 在这里初始化模组内容
			// Initalize your mod content here
		}
	}
}