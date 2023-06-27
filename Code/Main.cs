using Cultivation_Way.Addon;
using NCMS;
using UnityEngine;

namespace Addon_Namespace
{
    [ModEntry]
    public class AddonMainClass : CW_Addon
    {
        public override void awake()
        {
            // 不要在此处添加代码，除非你知道你在做什么
            // DO NOT code here.
            // 设置适配的核心版本为1.1.0
            set_adapted_core_version("1.1.0");
        }

        public override void initialize()
        {
            log("Hello CW Addon!");
            // 在这里初始化模组内容
            // Initalize your mod content here
            add_example_spell();
        }


        private void add_example_spell()
        {
        }

        private static void example_spell_end_action(
            int cur_frame_idx,
            ref Vector2 src_vec, ref Vector2 dst_vec,
            Cultivation_Way.Animation.SpriteAnimation anim)
        {
        }
    }
}