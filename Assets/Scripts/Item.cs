using UnityEngine;

    public class Item:MonoBehaviour
    {
        public static Item Instance;
        public string name;
        public int number;
        public Sprite image;
        public bool finded;

        public Item(string name, string imagePath)
        {
            this.name = name;
            number = 0;
            image=Resources.Load<Sprite>(imagePath);
            finded = false;
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
                number -= 1;
            }
        }

        public string get_name()
        {
            return name;
        }

    }
