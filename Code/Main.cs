using System.Collections.Generic;
using System.Linq;
using Cultivation_Way.Addon;
using Cultivation_Way.Constants;
using Cultivation_Way.General.AboutSpell;
using Cultivation_Way.Library;
using Cultivation_Way.Utils;
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


        private static void add_example_spell()
        {
            // 添加一个简单的示例法术
            CW_SpellAsset spell_asset = FormatSpells.create_on_target_attack_spell(
                "example_spell", "example_spell_anim", "effects/example", 0.015f,
                1, frame_action: example_spell_frame_action, target_type: SpellTargetType.TILE,
                spell_cost_list: new KeyValuePair<string, float>[]
                {
                    new("wakan", 30f)
                }
            );
        }

        private static void example_spell_frame_action(
            int cur_frame_idx,
            ref Vector2 src_vec, ref Vector2 dst_vec,
            Cultivation_Way.Animation.SpriteAnimation anim)
        {
            if (anim.src_object == null || !anim.src_object.isAlive()) return;
            WorldTile center_tile = World.world.GetTile((int)dst_vec.x, (int)dst_vec.y);
            if (center_tile == null) return;

            List<WorldTile> tiles = GeneralHelper.get_tiles_in_circle(center_tile, 1);

            anim.data.get(DataS.spell_cost, out float cost);

            foreach (WorldTile tile in tiles)
            {
                if (tile.building != null && GeneralHelper.is_enemy(anim.src_object, tile.building))
                    tile.building.getHit(cost, pType: (AttackType)CW_AttackType.Spell, pAttacker: anim.src_object);

                foreach (Actor actor in tile._units.Where(actor =>
                             actor.isAlive() && GeneralHelper.is_enemy(anim.src_object, actor)))
                {
                    actor.getHit(cost, pAttackType: (AttackType)CW_AttackType.Spell, pAttacker: anim.src_object);
                }
            }

            anim.src_object.base_data.health += (int)cost;
            if (anim.src_object.base_data.health > anim.src_object.stats[S.health])
            {
                anim.src_object.base_data.health = (int)anim.src_object.stats[S.health];
            }
        }
    }
}