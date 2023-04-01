using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Slime
{
    public class ResolutionManager : MonoBehaviour
    {
        [HideInInspector] public static int ResolutionX, ResolutionY;
        [SerializeField] List<BoxCollider2D> Walls;
        static int Offset = 500;
        float DeltaTime = 0;
        float UpdateTime = 1;

        private void Awake()
        {
            SetResolution();
        }

        private void Update()
        {
            DeltaTime += Time.deltaTime;
            
            if (DeltaTime >= UpdateTime)
            {
                if (ResolutionX != Screen.width || ResolutionY != Screen.height)
                {
                    SetResolution();
                }
                DeltaTime = 0;
            }
        }

        public void SetResolution()
        {
            ResolutionX = Screen.width;
            ResolutionY = Screen.height;

            SetWall();
        }

        void SetWall()
        {
            Walls[0].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-Offset, ResolutionY * 0.5f, 0));
            Walls[1].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX * 0.5f, Offset + ResolutionY, 0));
            Walls[2].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Offset + ResolutionX, ResolutionY * 0.5f, 0));
            Walls[3].transform.position = Camera.main.ScreenToWorldPoint(new Vector3(ResolutionX * 0.5f, -Offset, 0));

            Walls[0].size = new Vector2(1, ResolutionY * 0.5f);
            Walls[1].size = new Vector2(ResolutionX * 0.5f, 1);
            Walls[2].size = new Vector2(1, ResolutionY * 0.5f);
            Walls[3].size = new Vector2(ResolutionX * 0.5f, 1);
        }
    }
}

