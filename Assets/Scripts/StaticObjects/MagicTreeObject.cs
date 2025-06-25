using Inventory;
using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticObjects
{
    public class MagicTree : MonoBehaviour
    {
        public static MagicTree Instance;

        [FormerlySerializedAs("inside_spell")] [SerializeField]
        private Spells.Spell insideSpell;
        public Spells.Spell InsideSpell { get => insideSpell; set => insideSpell = value; }
        public bool discovered;
    
        public void search_tree()
        {
            if (discovered == false)
            {
                if (insideSpell != null)
                {
                    if (PlayerManager.Instance.learnSpellSkill)
                    {
                        InventoryManager.Instance.learn_spell(insideSpell);
                        discovered = true;
                        PlayerManager.Instance.IsMoving = true;
                    }
                    else
                    {
                        PlayerManager.Instance.IsMoving = true;
                    }
                }
            }

            PlayerManager.Instance.NewItem = false;
            PlayerManager.Instance.IsMoving = true;
        }

        public void Awake()
        {
            Instance=this;
            if (discovered)
            {
                // inside_spell.SetActive(true);
                //inside_spell=Resources.Load<SpellBase>("");
                //this.GetComponent<SpriteRenderer>().sprite =sprite;
            }
        }
    }
}