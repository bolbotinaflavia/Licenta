using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    public class MagicTree : MonoBehaviour
    {
        public static MagicTree Instance;
        [FormerlySerializedAs("inside_spell")] [SerializeField] private Spell  insideSpell;
        [SerializeField]private Spells.Spell insideSpell2;
        public bool discovered;
    
        public void search_tree()
        {
            if (discovered == false)
            {
                discovered = true;

                Debug.Log(insideSpell != null ? insideSpell.name : "spell not loaded");
                if (insideSpell != null)
                {
                    //Debug.Log("Learning spell");
                    //spell.SetActive(true);
                    PlayerManager.Instance.isMoving = true;
                    InventoryManager.Instance.learn_spell(insideSpell2);
                }
                //pentru alte obiecte
            }
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