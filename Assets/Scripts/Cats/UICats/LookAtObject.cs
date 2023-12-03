using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{
    public class LookAtObject : MonoBehaviour
    {
        private GameObject _object;

        public void Start()
        {
            _object = GameObject.Find("Camera");
        }

        // Update is called once per frame
        void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position - _object.transform.position);
        }
    }
}
