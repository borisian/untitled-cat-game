using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats.data
{
    public class CatData
    {
        [Header("Cat's description")]
        public string catName;
        //public string description;
        public int age;
        //public bool adopted;
        public string nameAnimatorController;

        /*[Header("Skin's cat")]
        public string skinName;
        public string body;
        public string oreille;
        public string head;
        public string queue;

        public CatData(string catName, int age, string skinName, string body, string oreille, string head, string queue)
        {
            this.catName = catName;
           // this.description = description;
            this.age = age;
            this.skinName = skinName;
            this.body = body;
            this.oreille = oreille;
            this.head = head;
            this.queue = queue;
        }*/

        public CatData(string catName, int age, string nameAnimatorController)
        {
            this.catName = catName;
            this.age = age;
            this.nameAnimatorController = nameAnimatorController;
        }
    }
}
