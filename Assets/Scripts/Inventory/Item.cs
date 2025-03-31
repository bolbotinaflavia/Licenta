using UnityEngine;

    public class Item:MonoBehaviour
    {
        public static Item Instance;
        public string name;
        public int number;
        public Sprite image;
        public bool finded;
        public float hp;
        public Item(string name, string imagePath, float hp)
        {
            this.name = name;
            number = 0;
            image=Resources.Load<Sprite>(imagePath);
            finded = false;
            this.hp = hp;
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                //Destroy(gameObject);
                return;
            }
       
        }

        public void find_first_item()
        {
            number += 1;
            finded = true;
        }
        public void find_item()
        {
            number += 1;
        }

        public void consume()
        {
            if (number > 0)
            {
                PlayerManager.Instance.eating = true;
                Hp_slider.Instance.UpdateUI(); 
                number -= 1;
                Invoke("eating_animation",2f);
            }
        }
        private void eating_animation()
        {
            Debug.Log("Animation started");
            PlayerManager.Instance.eating = false;
            //IsMoving = true;
        
        }
        public string get_name()
        {
            return name;
        }

    }
